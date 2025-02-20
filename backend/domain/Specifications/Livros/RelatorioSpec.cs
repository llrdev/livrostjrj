using System;
using System.Linq.Expressions;

namespace Domain.Specifications.Siaf
{

    public class RelatorioSpec : BaseSpecification<Entities.Livros.RelatorioLivro>
    {
        
        public RelatorioSpec() : base()
        {
            ApplyOrderBy(x => x.LivroId);
        }
    }
}