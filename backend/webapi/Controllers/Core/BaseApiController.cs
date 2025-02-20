using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Webapi.Core.Enums;

namespace Webapi.Controllers.Core
{
    /// <summary>
    /// Controlador base
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[ApiAuthorization]
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Extrai do usuario autenticado o login e nome.
        /// </summary>
        protected List<string> LoginNome()
        {
            return User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Extrai do usuario autenticado as roles.
        /// </summary>
        /// <returns></returns>
        protected List<string> GetRolesUsuario()
        {
            return User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Extrai do usuario autenticado as roles.
        /// </summary>
        /// <returns></returns>
        protected List<string> GetEmailUser()
        {
            return User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Recupera os parametros setados na query string e adiciona auditoria
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, string> GetParametros(string version = "1.0")
        {
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            foreach (var key in Request.Query.Keys)
            {
                parametros.Add(key, Request.Query[key]);
            }

            var loginNome = LoginNome();

            if (loginNome.Any())
            {
                parametros.Add("auditoria", $"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}. Emitido por {loginNome.Aggregate((i, j) => i + " - " + j)}");
                parametros.Add("version", version);
            }

            return parametros;
        }

        /// <summary>
        /// Recupera a extensão e mime type para o tipo de arquivo passado
        /// </summary>
        /// <param name="ft"></param>
        protected (string extensao, string mimetype) GetExtensaoEMimeType(EnumFileType ft)
        {
            if (EnumFileType.csv == ft) return (".csv", "text/csv");
            else if (EnumFileType.docx == ft) return (".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            else if (EnumFileType.html == ft) return (".html", "text/html");
            else if (EnumFileType.pdf == ft) return (".pdf", "application/pdf");
            else if (EnumFileType.txt == ft) return (".txt", "text/plain");
            else if (EnumFileType.xls == ft) return (".xls", "application/vnd.ms-excel");
            else if (EnumFileType.xlsx == ft) return (".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            return (null, null);
        }
    }
}
