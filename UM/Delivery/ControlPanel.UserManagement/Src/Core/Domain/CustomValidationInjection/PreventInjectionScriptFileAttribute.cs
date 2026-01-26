using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml;

namespace Domain.CustomValidationInjection
{
    public class PreventInjectionScriptFileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var file = value as IFormFile;
            if (file == null || file.Length == 0)
                return ValidationResult.Success;

            using var reader = new StreamReader(file.OpenReadStream());
            string content = reader.ReadToEnd();

            return ContainsOnlyPTagsWithoutEvents(content);
        }
        private ValidationResult ContainsOnlyPTagsWithoutEvents(string content)
        {
            string[] forbiddenTags = { "<script", "<embed", "<object", "<img", "<iframe" };
            string scriptPattern = @"<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>";
            string eventPattern = @"on\w+\s*=\s*['""][^'""]+['""]";
            string[] forbiddenPdfMarkers = { "/JavaScript", "/JS", "/OpenAction", "/AA" };

            foreach (var keyword in forbiddenPdfMarkers)
            {
                if (content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"File contains embedded JavaScript or unsafe PDF action: {keyword}");
                }
            }
            // Check for forbidden tags
            foreach (var tag in forbiddenTags)
            {
                if (content.Contains(tag, StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult($"The file contains disallowed HTML tag: {tag}");
                }
            }

            // Check for script blocks or inline event handlers
            if (Regex.IsMatch(content, scriptPattern, RegexOptions.IgnoreCase) ||
                Regex.IsMatch(content, eventPattern, RegexOptions.IgnoreCase))
            {
                return new ValidationResult("The file contains script content or unsafe event attributes.");
            }

            return ValidationResult.Success;
        }
    }
}
