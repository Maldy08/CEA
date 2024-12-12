using CEA.Domain.Common.Interfaces;
using CEA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Persistence.Configurations
{
    internal class DeptoUeConfiguration : IEntityTypeConfiguration<DeptoUe>
    {
        public void Configure(EntityTypeBuilder<DeptoUe> builder)
        {
            builder.HasKey(e => e.IdCea);
            builder.ToTable("DEPTOS_UE");

            builder.Property(e => e.Id).HasPrecision(3).HasColumnName("ID");
            builder.Property(e => e.IdCea).HasPrecision(3).HasColumnName("ID_CEA");
            builder.Property(e => e.IdShpoa).HasPrecision(3).HasColumnName("ID_SHPOA");
            builder.Property(e => e.Descripcion).HasMaxLength(100).HasColumnName("DESCRIPCION");
            builder.Property(e => e.Nivel).HasPrecision(1).HasColumnName("NIVEL");
            builder.Property(e => e.Oficial).HasPrecision(1).HasColumnName("OFICIAL");
            builder.Property(e => e.IdReporta).HasPrecision(3).HasColumnName("ID_REPORTA");
            builder.Property(e => e.AgrupaPoa).HasPrecision(1).HasColumnName("AGRUPA_POA");
            builder.Property(e => e.Meta).HasPrecision(2).HasColumnName("META");
            builder.Property(e => e.Accion).HasPrecision(2).HasColumnName("ACCION");
            builder.Property(e => e.Prog).HasPrecision(3).HasColumnName("PROG");
            builder.Property(e => e.EmpRespon).HasPrecision(4).HasColumnName("EMP_RESPON");
            builder.Property(e => e.AgrupaDir).HasPrecision(3).HasColumnName("AGRUPDIR");
        }
    }
}
