using Domain.Interfaces;
using Infra.Data.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Extensions
{
    public static class SqlExtensions
    {
        /// <summary>
        /// Adiciona ao service collection o contexto do sql server.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="connectionString">Conexão com do banco</param>
        public static void UseSqlServer([NotNull] this IServiceCollection serviceCollection, [NotNull] string connectionString)
        {

            //SQL
            serviceCollection.AddDbContext<SqlServerDbContext>(options => options.UseSqlServer(connectionString: connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(300)));

            //Unit of work
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}