using System;
using System.Linq.Expressions;

namespace Domain.Specifications.Siaf
{

    public class LivroAssuntoSpec : BaseSpecification<Entities.Livros.LivroAssunto>
    {

        public LivroAssuntoSpec(int codigoLivro) : base(x => x.LivroCodiAs == codigoLivro)
        {
            ApplyOrderBy(x => x.LivroCodiAs);
        }
        
        public LivroAssuntoSpec() : base()
        {
            ApplyOrderBy(x => x.LivroCodiAs);
        }
    }
}