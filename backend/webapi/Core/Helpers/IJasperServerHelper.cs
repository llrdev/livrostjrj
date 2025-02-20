using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Webapi.Core.Helpers
{
    /// <summary>
    /// Auxiliar para trabalhar com relatorios no jasper server
    /// </summary>
    public interface IJasperServerHelper
    {
        /// <summary>
        /// Realiza o login no jasper server
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        Task<(string sessionId, HttpStatusCode statusCode, string error)> Logar(string usuario, string senha);

        /// <summary>
        /// Fecha a sessão no jasper server. 
        /// E boa prática ao realizar o logoff do usuário na aplicação realizar o fechamento da sessão no jasper server.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task<(bool success, HttpStatusCode statusCode, string error)> Logout(string sessionId);

        /// <summary>
        /// Recupera o relatório no jasper server
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        Task<(byte[] report, HttpStatusCode statusCode, string error)> Get(string sessionId, Dictionary<string, string> parametros);
    }
}
