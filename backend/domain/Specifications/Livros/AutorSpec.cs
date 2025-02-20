using System;
using System.Linq.Expressions;

namespace Domain.Specifications.Siaf
{

    public class AutorSpec : BaseSpecification<Entities.Livros.Autor>
    {

        public AutorSpec(int codAutor) : base(x => x.CodAu == codAutor)
        {
            ApplyOrderBy(x => x.CodAu);
        }
        
        public AutorSpec() : base()
        {
            ApplyOrderBy(x => x.CodAu);
        }
    }
}