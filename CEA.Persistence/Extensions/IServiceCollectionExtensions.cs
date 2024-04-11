using CEA.Application.Interfaces.Repositories;
using CEA.Persistence.Context;
using CEA.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CEA.Persistence.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMappings();
            services.AddDbContext(configuration);
            services.AddRepositories();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("OracleConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                
                 options.UseOracle(connectionString,
                     builder =>
                     {
                         builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                         builder.UseOracleSQLCompatibility("11");
                     }));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient<IViaticoRepository, ViaticoRepository>()
                .AddTransient<IViaticoPorEmpleadoDto, ViaticosPorEmpleado>();
               
        }
    }
}
