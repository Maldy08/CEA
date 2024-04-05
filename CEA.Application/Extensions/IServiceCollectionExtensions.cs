using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace CEA.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR( cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        }

        private static void AddValidators(this IServiceCollection services)
        {
          services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
