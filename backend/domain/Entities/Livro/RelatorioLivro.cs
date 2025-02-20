using Domain.Core;

namespace Domain.Entities.Livros
{
    public partial class RelatorioLivro : BaseEntity
    {
        public int LivroId { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string Assuntos { get; set; }
    }
}
