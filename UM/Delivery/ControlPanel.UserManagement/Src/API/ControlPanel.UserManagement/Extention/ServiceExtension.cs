
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RateLimiting;
using System.Globalization;
using System.Threading.RateLimiting;


namespace _4EPlatform_StrategicTelecom_Thanks.Extension
{
    public static class ServiceExtension
    {
        public static void AddRatesLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRateLimiter(options =>
            {
                var rateLimitSection = configuration.GetSection("RateLimit");
                var permitLimit = rateLimitSection.GetValue<int>("PermitLimit");
                var queueLimit = rateLimitSection.GetValue<int>("QueueLimit");
                var windowSeconds = rateLimitSection.GetValue<int>("WindowSeconds");
                options.AddFixedWindowLimiter("FixedWindowPolicy", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(windowSeconds);
                    opt.PermitLimit = permitLimit;
                    opt.QueueLimit = queueLimit;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                }).RejectionStatusCode = 429;
            });
        }

    }
}
