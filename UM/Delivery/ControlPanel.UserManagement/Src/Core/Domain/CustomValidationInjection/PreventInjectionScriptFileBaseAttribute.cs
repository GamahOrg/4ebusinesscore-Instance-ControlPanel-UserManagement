using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Domain.CustomValidationInjection
{
    public class PreventInjectionScriptFileBaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            var base64String = value as string;
            if (string.IsNullOrWhiteSpace(base64String))
                return ValidationResult.Success;

            // Remove data URI prefix if present
            if (base64String.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
            {
                int commaIndex = base64String.IndexOf(',');
                if (commaIndex >= 0)
                    base64String = base64String.Substring(commaIndex + 1);
            }

            string decodedContent;

            try
            {
                // Normalize base64 (remove whitespace or line breaks)
                base64String = base64String.Trim().Replace(" ", "").Replace("\n", "").Replace("\r", "");

                // Decode base64 to string
                byte[] data = Convert.FromBase64String(base64String);
                decodedContent = Encoding.UTF8.GetString(data);
            }
            catch (FormatException)
            {
                return new ValidationResult("The input is not a valid base64-encoded string.");
            }

            // Disallowed HTML tags or JS
            string[] forbiddenTags = { "<script", "<embed", "<object", "<img", "<iframe" };
            string scriptPattern = @"<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>";
            string eventPattern = @"on\w+\s*=\s*['""][^'""]+['""]";
            string[] forbiddenPdfMarkers = { "/JavaScript", "/JS", "/OpenAction", "/AA" };

            foreach (var keyword in forbiddenPdfMarkers)
            {
                if (decodedContent.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"File contains embedded JavaScript or unsafe PDF action: {keyword}");
                }
            }
            foreach (var tag in forbiddenTags)
            {
                if (decodedContent.Contains(tag, StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"Content contains disalContent must include at least onelowed HTML tag: {tag}");
                }
            }

            if (Regex.IsMatch(decodedContent, scriptPattern, RegexOptions.IgnoreCase) ||
                Regex.IsMatch(decodedContent, eventPattern, RegexOptions.IgnoreCase))
            {
                return new ValidationResult("Content contains script or unsafe inline JS.");
            }

            return ValidationResult.Success;
        }
    }
}
