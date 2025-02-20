using Domain.Interfaces.Infra;
using Infra.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Extensions
{
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Adiciona ao service collection auto mapper e o servico.
        /// </summary>
        public static void UseAutoMapper([NotNull] this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(MyMapper));
            serviceCollection.AddScoped<IMyMapper, MyMapper>();
        }
    }
}
