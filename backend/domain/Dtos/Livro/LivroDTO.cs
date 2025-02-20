using System.Collections.Generic;

namespace Domain.Dtos.Livro
{
    public class LivroDTO
    {
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string AnoPublicacao { get; set; }
        public decimal Valor { get; set; }
        public List<AutorDto> AutorCodAus { get; set; } = new List<AutorDto>();
        public List<AssuntoDto> AssuntoCodAs { get; set; } = new List<AssuntoDto>();
    }

    public class AutorDto
    {
        public string Nome { get; set; }
    }

    public class AssuntoDto
    {
        public string Descricao { get; set; }
    }
}
