using System;
using System.Linq.Expressions;

namespace Domain.Specifications.Siaf
{

    public class AssuntoSpec : BaseSpecification<Entities.Livros.Assunto>
    {

        public AssuntoSpec(int codigoAssunto) : base(x => x.CodAs == codigoAssunto)
        {
            ApplyOrderBy(x => x.CodAs);
        }
        
        public AssuntoSpec() : base()
        {
            ApplyOrderBy(x => x.CodAs);
        }
    }
}