using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Webapi.Core.Helpers
{
    public class JasperServerHelper : IJasperServerHelper
    {
        /*
         * Neste caso não foi implementado conforme orientação da documentação da microsoft:
         * https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
         * Ao usar a abordagem acima apresentava erro pois criava alguma referencia a cookies anteriores pois quem controla o ciclo de vida do objeto http client é a factory. E ao tentar chamar o login seguidas vezes não trazia novo cookie.
         * Devido a este comportamento foi necessário a cada chamada criar a instancia do http client e depois dispor dela. 
         */

        private readonly string _urlJasperServer;

        private static readonly string PATH_REPORTS = "rest_v2/reports/";
        private static readonly string PATH_LOGIN = "rest_v2/login";
        private static readonly string PATH_LOGOUT = "logout.html";

        public JasperServerHelper(string urlJasperServer)
        {
            _urlJasperServer = urlJasperServer;
        }

        public async Task<(string sessionId, HttpStatusCode statusCode, string error)> Logar(string usuario, string senha)
        {
            var uri = $"{_urlJasperServer}{PATH_LOGIN}?j_username={usuario}&j_password={senha}";

            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Clear();

            var result = await httpClient.GetAsync(uri);
            var statusCode = result.StatusCode;
            var responseString = await result.Content.ReadAsStringAsync();

            if (statusCode == HttpStatusCode.OK)
            {
                var cookie = result.Headers.GetValues("Set-Cookie").ToList();
                var sessionId = cookie.Select(i => i.Split(";")[0]).Aggregate("", (acc, x) => acc += $"{x};");
                return (Base64Encode(sessionId), statusCode, null);
            }

            return (null, statusCode, responseString);
        }

        public async Task<(bool success, HttpStatusCode statusCode, string error)> Logout(string sessionId)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Clear();

            var cookie = Base64Decode(sessionId);

            httpClient.DefaultRequestHeaders.Add("Cookie", cookie);

            var result = await httpClient.GetAsync($"{_urlJasperServer}{PATH_LOGOUT}");
            var statusCode = result.StatusCode;
            var responseString = await result.Content.ReadAsStringAsync();

            if (statusCode == HttpStatusCode.OK)
            {
                return (true, statusCode, null);
            }

            return (false, statusCode, responseString);
        }

        public async Task<(byte[] report, HttpStatusCode statusCode, string error)> Get(string sessionId, Dictionary<string, string> parametros)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Clear();

            var cookie = Base64Decode(sessionId);

            httpClient.DefaultRequestHeaders.Add("Cookie", cookie);

            var url = MontarUrlRelatorio(parametros);

            var result = await httpClient.GetAsync(url);
            var statusCode = result.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
                var response = await result.Content.ReadAsByteArrayAsync();
                return (response, statusCode, null);
            }

            var responseString = await result.Content.ReadAsStringAsync();

            return (null, statusCode, responseString);
        }

        #region Métodos Privados
        /// <summary>
        /// Encode
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decode
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Extrai os parametros e monta a url do relatorio
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        private string MontarUrlRelatorio(Dictionary<string, string> parametros)
        {

            var parametrosConfig = new List<string> { "resource", "fileType", "sessionId" };

            var resource = parametros[parametrosConfig[0]];
            var fileType = parametros[parametrosConfig[1]];
            var query = "";

            foreach (var key in parametros.Keys)
            {
                if (!parametrosConfig.Contains(key))
                {
                    query += $"{key}={parametros[key]}&";
                }
            }

            return $"{_urlJasperServer}{PATH_REPORTS}{resource}.{fileType}?{query}";
        }
        #endregion
    }
}
