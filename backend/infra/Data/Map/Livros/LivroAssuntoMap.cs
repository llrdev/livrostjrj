using Domain.Entities.Livros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Infra.Data.Map.Sync
{
    public class LivroAssuntoMap : IEntityTypeConfiguration<LivroAssunto>
    {
        public void Configure(EntityTypeBuilder<LivroAssunto> entity)
        {
            entity.HasKey(e => new { e.LivroCodiAs, e.AssuntoCodAs }).HasName("PK_Livro_Assunto");

            entity.ToTable("Livro_Assunto");

            entity.Property(e => e.AssuntoCodAs)
                .HasColumnName("Assunto_CodAs");

            entity.Property(e => e.LivroCodiAs)
                 .HasColumnName("Livro_Codi_As");

        }
    }
}
