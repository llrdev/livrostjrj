using System;
using System.Collections.Generic;

namespace Domain.Dtos.Livro
{
    public class LivroFlatDTO
    {
        public int Codi { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string AnoPublicacao { get; set; }
        public decimal Valor { get; set; }
        public ICollection<AutorFlatDto> AutorCodAus { get; set; }
        public ICollection<AssuntoFlatDto> AssuntoCodAs { get; set; }
    }

    public class AutorFlatDto
    {
        public int CodAu { get; set; }
        public string Nome { get; set; }
    }

    public class AssuntoFlatDto
    {
        public int CodAs { get; set; }
        public string Descricao { get; set; }
    }
}
