using Domain.Core;
using System.Collections.Generic;

namespace Domain.Entities.Livros
{
    public partial class Autor : BaseEntity
    {
        public int CodAu { get; set; }

        public string Nome { get; set; }

        public virtual ICollection<Livro> LivroCodiAus { get; set; } = new List<Livro>();
    }
}
