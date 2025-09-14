using Janet.Core.Services;
using Janet.Core.Services.Interfaces;
using Janice.Core.Services;
using Janice.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Janice.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJaniceCoreServices(this IServiceCollection services, string configPath)
        {
            // Register ConfigService as a singleton
            services.AddSingleton<IConfigService>(provider => new ConfigService(configPath));

            // Register HttpClient for OllamaApiService
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IOllamaApiService, OllamaApiService>();

            // Register UserService and LoginService
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ILoginService, LoginService>();

            // Register IntentService
            services.AddSingleton<IIntentService, IntentService>();

            return services;
        }
    }
}
