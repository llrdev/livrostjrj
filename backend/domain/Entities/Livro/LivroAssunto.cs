using Domain.Core;
using Domain.Dtos.Livro;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.Livros
{
    public partial class LivroAssunto : BaseEntity
    {
        public int LivroCodiAs { get; set; }
        public int AssuntoCodAs { get; set; }
    }
}