using Domain.Core;
using Domain.Dtos.Livro;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.Livros
{
    public partial class LivroAutor : BaseEntity
    {
        public int LivroCodiAu { get; set; }
        public int AutorCodAu { get; set; } 
    }
}