

using CEA.Application.DTOs;
using CEA.Domain.Common;
using CEA.Domain.Common.Interfaces;
using CEA.Domain.Entities.Transparencia;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CEA.Persistence.Context
{
    public class ApplicationDbContextSQL: DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public ApplicationDbContextSQL(DbContextOptions<ApplicationDbContextSQL> options, IDomainEventDispatcher dispatcher) : base(options)
        {
            _dispatcher = dispatcher;
        }

        public DbSet<AccesoReporte> AccesoReporte => Set<AccesoReporte>();
        public DbSet<Reporte> Reporte => Set<Reporte>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<BitacoraArchivo> BitacoraArchivo => Set<BitacoraArchivo>();

        //public DbSet<GetFormatoByUserIdDto> GetFormatoByUserId  => Set<GetFormatoByUserIdDto>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
