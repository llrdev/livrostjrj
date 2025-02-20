using Domain.Core;
using Domain.Dtos.Livro;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.Livros
{
    public partial class Livro : BaseEntity
    {
        public int Codi { get; set; }

        public string Titulo { get; set; }

        public string Editora { get; set; }

        public string AnoPublicacao { get; set; }

        public decimal? Valor { get; set; }

        public virtual ICollection<Assunto> AssuntoCodAs { get; set; } = new List<Assunto>();

        public virtual ICollection<Autor> AutorCodAus { get; set; } = new List<Autor>();

   
    }
}