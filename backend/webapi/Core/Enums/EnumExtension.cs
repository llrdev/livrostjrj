using System;
using System.ComponentModel;
using System.Reflection;

namespace Webapi.Core.Enums
{
    public static class EnumExtension
    {
        /// <summary>
        /// Retorna a descrição de um enumerador
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionEnum(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
