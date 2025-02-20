using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Webapi.Core.Enums;

namespace Webapi.Core.Attributes
{
    /// <summary>
    /// Autorização customizada
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params Roles[] roles)
        {
            if (roles.Any())
            {
                var _roles = string.Join(",", roles.Select(x => x.GetDescriptionEnum()).ToList());
                if (!string.IsNullOrEmpty(_roles)) Roles = _roles;
            }
        }
    }
}
