using Domain.Entities.Livros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Infra.Data.Map.Sync
{
    public class AutorMap : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> entity)
        {
            entity.HasKey(e => e.CodAu).HasName("PK__Autor");

            entity.ToTable("Autor");

            //entity.Property(e => e.CodAu).ValueGeneratedNever();
            entity.Property(e => e.Nome)
                .HasMaxLength(40)
                .IsUnicode(false);
        }
    }
}
