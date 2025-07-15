using CEA.Application.DTOs;
using CEA.Application.DTOs.Oficios;
using CEA.Application.DTOs.Vehiculos;
using CEA.Application.DTOs.Viaticos;
using CEA.Domain.Common;
using CEA.Domain.Common.Interfaces;
using CEA.Domain.Entities;
using CEA.Domain.Entities.Oficios;
using CEA.Domain.Entities.RecursosHumanos;
using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace CEA.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDomainEventDispatcher dispatcher) : base(options)
        {
            _dispatcher = dispatcher;
        }

        //General
        public DbSet<User> Usuarios => Set<User>();

        //Viaticos
        public DbSet<Viatico> Viatico => Set<Viatico>();
        public DbSet<ViaticoCiudad> ViaticoCiudad => Set<ViaticoCiudad>();
        public DbSet<ViaticosPorEmpleadoDto> ViaticosPorEmpleadosDto => Set<ViaticosPorEmpleadoDto>();
        public DbSet<ViaticoPais> ViaticoPais => Set<ViaticoPais>();
        public DbSet<ViaticoOfi> ViaticoOficina => Set<ViaticoOfi>();
        public DbSet<ViaticoPart> ViaticoPart => Set<ViaticoPart>();
        public DbSet<ViaticoDetalleDto> ViaticoDetalleDto => Set<ViaticoDetalleDto>();

        //Transparencia
        public DbSet<FormatoComisionDto> FormatoComisionDto => Set<FormatoComisionDto>();

        //Recursos Humanos
        public DbSet<Empleado> Empleado => Set<Empleado>();
        public DbSet<DeptoUe> DeptoUe => Set<DeptoUe>();

        //Vehiculos
        public DbSet<VsWtVehiculosDto> VsWtVehiculos => Set<VsWtVehiculosDto>();
        public DbSet<VsListaVehiculosDto> VsListaVehiculos => Set<VsListaVehiculosDto>();

        //Oficios
        public DbSet<Oficio> Oficio => Set<Oficio>();
        public DbSet<OficioDto> OficioDto => Set<OficioDto>();
        public DbSet<OficioEstatus> OficioEstatus => Set<OficioEstatus>();
        public DbSet<OficioBitacora> OficioBitacora => Set<OficioBitacora>();
        public DbSet<OficioResponsable> OficioResponsable => Set<OficioResponsable>();
        public DbSet<OficioUsuExt> OficioUsuExt => Set<OficioUsuExt>();
        public DbSet<OficioUsuExtDto> OficioUsuExtDto => Set<OficioUsuExtDto>();
        public DbSet<OficioXexpedir> OficioXexpedir => Set<OficioXexpedir>();
        public DbSet<OficioParametro> OficioParametro => Set<OficioParametro>();
        public DbSet<OficioGpi> OficioGpi => Set<OficioGpi>();
        public DbSet<DeptoCeaSeproaDto> deptoCeaSeproaDto => Set<DeptoCeaSeproaDto>();


        //Oficios Functions
        public DbSet<OficioDtoFunction> OficioDtoFunction => Set<OficioDtoFunction>();
        public DbSet<OficioListaDashboardDto> oficioListaDashboardDtos => Set<OficioListaDashboardDto>();
        public DbSet<OficioContadoresDashboardDto> oficioContadoresDashboardDtos => Set<OficioContadoresDashboardDto>();
        public DbSet<OficioListaDepartamentosDto> oficioListaDepartamentosDtos => Set<OficioListaDepartamentosDto>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema("CEA");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
