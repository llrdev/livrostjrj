using Domain.Entities.Livros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Infra.Data.Map.Sync
{
    public class LivrosMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> entity)
        {
            entity.HasKey(e => e.Codi);

            entity.ToTable("Livro");

            entity.Property(e => e.AnoPublicacao)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Editora)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("money");

           
        }
    }
}
