using System;
using System.Collections.Generic;

namespace Domain.Dtos.Livro
{
    public class RelatorioDTO
    {
        public int LivroId { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string Assuntos { get; set; }
    }
}
