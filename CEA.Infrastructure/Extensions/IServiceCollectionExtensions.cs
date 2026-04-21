using CEA.Application.Services;
using CEA.Domain.Common;
using CEA.Domain.Common.Interfaces;
using CEA.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;


namespace CEA.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Obtenemos la sección
            var apiSettings = configuration.GetSection("ExternalApis");

            // 2. Leemos los valores DESDE LA SECCIÓN (apiSettings)
            var apiUser = apiSettings.GetValue<string>("ChecadorUser");
            var apiPassword = apiSettings.GetValue<string>("ChecadorPassword");

            // Esta función ahora tendrá los valores correctos
            Func<HttpClientHandler> createDigestHandler = () => new HttpClientHandler
            {
                Credentials = new NetworkCredential(apiUser, apiPassword),
               // PreAuthenticate = true
            };

            // (Esta parte ya estaba bien, pero la incluyo para el contexto)
            services.AddHttpClient("ChecadorMxli", client =>
            {
                client.BaseAddress = new Uri(apiSettings.GetValue<string>("ChecadorMxli"));
            })
            .ConfigurePrimaryHttpMessageHandler(createDigestHandler);

            services.AddHttpClient("ChecadorArct", client =>
            {
                client.BaseAddress = new Uri(apiSettings.GetValue<string>("ChecadorArct"));
            })
            .ConfigurePrimaryHttpMessageHandler(createDigestHandler);

            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IMediator, Mediator>()
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddTransient<IFileService, FileService>()
                .AddTransient<IFileServiceTransparencia, FileServiceTransparencia>()
                .AddTransient<IEmailService, EmailService>()
                .AddTransient<IAuthService, AuthService>()
                .AddTransient<IAuthServiceTransparencia, AuthServiceTransparencia>()
                .AddTransient<IGoogleCloudService, GoogleCloudService>()
                .AddTransient<IChecadorService, ChecadorService>();
        }
    }
}