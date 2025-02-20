using Domain.Core;
using System;

namespace Domain.Entities.Views
{
    public class LivroView : BaseEntity
    {
        public string Anoap { get; set; }
        public int Codap { get; set; }
        public string Flgbloqueio { get; set; }
        public string Idtercei { get; set; }
        public string Sigcttercei { get; set; }
        public string Numcttercei { get; set; }
        public decimal? Valliquido { get; set; }
        public DateTime? Datbase { get; set; }
        public DateTime? Datvencto { get; set; }
        public DateTime? Datpagto { get; set; }
        public string Tipdocagreg { get; set; }
        public string Numdocagreg { get; set; }
        public string Partdocagreg { get; set; }
    }
}
