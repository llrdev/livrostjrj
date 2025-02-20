using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Webapi.Core.Helpers;

namespace Webapi.Extensions
{
    public static class UriHelperExtensions
    {
        /// <summary>
        /// Injeção de dependencia do servico de uri
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void UseUriHelper([NotNull] this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddSingleton<IUriHelper>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriHelper(uri);
            });
        }
    }
}
