using CEA.Domain.Common;
using CEA.Domain.Common.Interfaces;
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

        public DbSet<Viatico> Viatico => Set<Viatico>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema("CEA");

            modelBuilder.Entity<Viatico>(entity =>
            {
                entity.HasKey(e => new { e.Oficina, e.Ejercicio, e.NoViat });
                entity.ToTable("VIATICOS");

                entity.Property(entity => entity.Id).HasColumnName("ID");
                entity.Property(e => e.Oficina).HasPrecision(1).HasColumnName("OFICINA");
                entity.Property(e => e.Ejercicio).HasPrecision(4).HasColumnName("EJERCICIO");
                entity.Property(e => e.NoViat).HasPrecision(5).HasColumnName("NOVIAT");
                entity.Property(e => e.Fecha).HasColumnType("DATE").HasColumnName("FECHA");
                entity.Property(e => e.NoEmp).HasPrecision(4).HasColumnName("NOEMP");
                entity.Property(e => e.OrigenId).HasPrecision(3).HasColumnName("ORIGENID");
                entity.Property(e => e.DestinoId).HasPrecision(3).HasColumnName("DESTINOID");
                entity.Property(e => e.Motivo).HasMaxLength(300).HasColumnName("MOTIVO");
                entity.Property(e => e.FechaSal).HasColumnType("DATE").HasColumnName("FECHASAL");
                entity.Property(e => e.FechaReg).HasColumnType("DATE").HasColumnName("FECHAREG");
                entity.Property(e => e.Dias).HasPrecision(2).HasColumnName("DIAS");
                entity.Property(e => e.InforFecha).HasColumnType("DATE").HasColumnName("INFOR_FECHA");
                entity.Property(e => e.InforAct).HasMaxLength(500).HasColumnName("INFOR_ACT");
                entity.Property(e => e.Nota).HasMaxLength(500).HasColumnName("NOTA");
                entity.Property(e => e.Estatus).HasPrecision(1).HasColumnName("ESTATUS");
                entity.Property(e => e.FechaMod).HasColumnType("DATE").HasColumnName("FECHAMOD");
                entity.Property(e => e.Pol).HasPrecision(4).HasColumnName("POL");
                entity.Property(e => e.PolMes).HasPrecision(2).HasColumnName("POLMES");
                entity.Property(e => e.Caja).HasPrecision(2).HasColumnName("CAJA");
                entity.Property(e => e.CajaVale).HasPrecision(5).HasColumnName("CAJA_VALE");
                entity.Property(e => e.CajaRepo).HasPrecision(5).HasColumnName("CAJA_REPO");
                entity.Property(e => e.NoEmpCrea).HasPrecision(4).HasColumnName("NOEMP_CREA");
                entity.Property(e => e.InforResul).HasMaxLength(500).HasColumnName("INFOR_RESUL");

            });

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
