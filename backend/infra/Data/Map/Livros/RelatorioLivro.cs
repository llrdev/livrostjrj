using Domain.Entities.Livros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Infra.Data.Map.Sync
{
    public class RelatorioLivroMap : IEntityTypeConfiguration<RelatorioLivro>
    {
        public void Configure(EntityTypeBuilder<RelatorioLivro> entity)
        {
            entity
                .HasNoKey()
                .ToView("RelatorioLivros");

            entity.Property(e => e.Assuntos)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Autor)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.LivroId).HasColumnName("LivroID");
            entity.Property(e => e.Titulo)
                .HasMaxLength(40)
                .IsUnicode(false);
        }
    }
}
