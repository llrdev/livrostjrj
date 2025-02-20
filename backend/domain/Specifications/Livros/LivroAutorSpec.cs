using System;
using System.Linq.Expressions;

namespace Domain.Specifications.Siaf
{

    public class LivroAutorSpec : BaseSpecification<Entities.Livros.LivroAutor>
    {

        public LivroAutorSpec(int codigoLivro) : base(x => x.LivroCodiAu == codigoLivro)
        {
            ApplyOrderBy(x => x.LivroCodiAu);
        }
        
        public LivroAutorSpec() : base()
        {
            ApplyOrderBy(x => x.LivroCodiAu);
        }
    }
}