using CEA.Application.Services;
using CEA.Domain.Common;
using CEA.Domain.Common.Interfaces;
using CEA.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace CEA.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();

        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IMediator, Mediator>()
                .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
                .AddTransient<IFileService, FileService>();
        }
    }
}
