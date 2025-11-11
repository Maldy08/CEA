using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.Interfaces.Repositories.Transparencia;
using CEA.Application.Interfaces.Repositories.Vehiculos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Persistence.Context;
using CEA.Persistence.Repositories;
using CEA.Persistence.Repositories.Nomina;
using CEA.Persistence.Repositories.Oficios;
using CEA.Persistence.Repositories.Transparencia;
using CEA.Persistence.Repositories.Vehiculos;
using CEA.Persistence.Repositories.Viaticos;
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
            var connectionStringSQL = configuration.GetConnectionString("SQLConnection");

            services.AddDbContext<ApplicationDbContext>(options =>

                 options.UseOracle(connectionString,
                     builder =>
                     {
                         builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                         builder.UseOracleSQLCompatibility("11");
                     }));

            services.AddDbContext<ApplicationDbContextSQL>(options =>
                options.UseSqlServer(connectionStringSQL,
                                   builder =>
                                   {
                                       builder.MigrationsAssembly(typeof(ApplicationDbContextSQL).Assembly.FullName);
                                   }));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IUnitOfWorkSQL), typeof(UnitOfWorkSQL))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient(typeof(IGenericRepositorySQL<>), typeof(GenericRepositorySQL<>))
                .AddTransient<IEmpleadoRepository, EmpleadoRepository>()
                .AddTransient<IDeptoRepository, DeptoRepository>()

                //Viaticos
                .AddTransient<IViaticoRepository, ViaticoRepository>()
                .AddTransient<IViaticoPorEmpleadoDto, ViaticosPorEmpleado>()
                .AddTransient<IViaticoPartRepository, ViaticoPartRepository>()
                .AddTransient<IFormatoComisionRepository, FormatoComisionRepository>()
                .AddTransient<IViaticoCiudadRepository, ViaticoCiudadRepository>()
                .AddTransient<IViaticoEstadoRepository, ViaticoEstadoRepository>()
                .AddTransient<IViaticoPaisRepository, ViaticoPaisRepository>()
                .AddTransient<IViaticoOficinaRepository, ViaticoOficinaRepository>()
                .AddTransient<IViaticoDetalleRepository, ViaticoDetalleRepository>()

                //Transparencia
                .AddTransient<ITransparenciaFormatoRepository, TransparenciaFormatoRepository>()
                .AddTransient<ITransparenciaBitachoraArchivoRepository, TransparenciaBitachoraArchivoRepository>()
                .AddTransient<IUserTransparenciaRepository, UserTransparenciaRepository>()
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IFormatoRepository, FormatoRepository>()

                //Vehiculos
                .AddTransient<IVsWtVehiculosRepository, VsWtVehiculosRepository>()
                .AddTransient<IVsListaVehiculosRepository, VsListaVehiculosRepository>()

                //Oficios
                .AddTransient<IOficioRepository, OficioRepository>()
                .AddTransient<IOficioBitacoraRepository, OficioBitacoraRepository>()
                .AddTransient<IOficioEstatusRepository, OficioEstatusRepository>()
                .AddTransient<IOficioUsuExtRepository, OficioUsuExtRepository>()
                .AddTransient<IOficioResponsableRepository, OficioResponsableRepository>()
                .AddTransient<IOficioParametroRepository, OficioParametroRepository>()
                .AddTransient<IOficioFunctions, OficioFunctions>()
                .AddTransient<IOficioGpiRepository, OficioGpiRepository>()

                //Nomina

                .AddTransient<INomPeriodosRepository, NomPeriodosRepository>(); 



        }
    }
}
