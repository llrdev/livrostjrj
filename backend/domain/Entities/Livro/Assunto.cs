using Domain.Core;
using System.Collections.Generic;

namespace Domain.Entities.Livros
{
    public partial class Assunto : BaseEntity
    {
        public int CodAs { get; set; }

        public string Descricao { get; set; }

        public virtual ICollection<Livro> LivroCodis { get; set; } = new List<Livro>();
    }
}
