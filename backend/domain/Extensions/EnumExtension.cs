﻿using System;
using System.ComponentModel;
using System.Reflection;


namespace Domain.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
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

        public static string GetDescriptionByEnum(Type type, object entity)
        {
            try
            {
                var memberInfo = type.GetMember(entity != null ? entity.ToString() : "");
                if (memberInfo?.Length > 0)
                {
                    var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attributes.Length > 0)
                    {
                        return ((DescriptionAttribute)attributes[0]).Description;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write($"Erro ao consultar a descrição: {ex.Message}");
            }

            return string.Empty;
        }

    }
}
