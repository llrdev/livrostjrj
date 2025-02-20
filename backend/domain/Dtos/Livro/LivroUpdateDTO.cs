using System.Collections.Generic;

namespace Domain.Dtos.Livro
{
    public class LivroUpdateDTO
    {
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string AnoPublicacao { get; set; }
        public decimal Valor { get; set; }
        public List<AutorUpdateDto> AutorCodAus { get; set; } = new List<AutorUpdateDto>();
        public List<AssuntoUpdateDto> AssuntoCodAs { get; set; } = new List<AssuntoUpdateDto>();
    }

    public class AutorUpdateDto
    {
        public int CodAu { get; set; }
        public string Nome { get; set; }
    }

    public class AssuntoUpdateDto
    {
        public int CodAs { get; set; }
        public string Descricao { get; set; }
    }
}
