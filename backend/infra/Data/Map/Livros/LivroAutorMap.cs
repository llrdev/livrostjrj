using Domain.Entities.Livros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Infra.Data.Map.Sync
{
    public class LivroAutorMap : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> entity)
        {
            entity.HasKey(e => new { e.LivroCodiAu, e.AutorCodAu }).HasName("PK_Livro_Autor");

            entity.ToTable("Livro_Autor");

            entity.Property(e => e.AutorCodAu)
                .HasColumnName("Autor_CodAu");

            entity.Property(e => e.LivroCodiAu)
                .HasColumnName("Livro_Codi_Au");
        }
    }
}
