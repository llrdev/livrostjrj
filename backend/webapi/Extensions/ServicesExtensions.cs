using Domain.Interfaces.Livro;
using Domain.Services.Livro;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Webapi.Extensions
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Injeção de dependencia dos servicos do domínio
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void UseDomainServices([NotNull] this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<ILivro, LivroService>();

        }
    }
}
