using System;
using System.Linq.Expressions;

namespace Domain.Specifications.Siaf
{

    public class LivrosSpec : BaseSpecification<Entities.Livros.Livro>
    {

        public LivrosSpec(int codigoLivro) : base(x => x.Codi == codigoLivro)
        {
            ApplyOrderBy(x => x.Codi);
        }
        
        public LivrosSpec() : base()
        {
            ApplyOrderBy(x => x.Codi);
        }
    }
}