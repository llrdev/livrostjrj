using System.ComponentModel;

namespace Webapi.Core.Enums
{
    /// <summary>
    /// Roles do sistema
    /// </summary>
    public enum Roles
    {
        [Description("admin")]
        ADMIN,
        [Description("user")]
        USUARIO,
    }
}
