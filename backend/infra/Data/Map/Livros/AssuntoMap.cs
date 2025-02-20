using Domain.Entities.Livros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Infra.Data.Map.Sync
{
    public class AssuntoMap : IEntityTypeConfiguration<Assunto>
    {
        public void Configure(EntityTypeBuilder<Assunto> entity)
        {
            entity.HasKey(e => e.CodAs).HasName("PK__Assunto");

            entity.ToTable("Assunto");

            //entity.Property(e => e.CodAs).ValueGeneratedNever();
            entity.Property(e => e.Descricao)
                .HasMaxLength(40)
                .IsUnicode(false);
        }
    }
}
