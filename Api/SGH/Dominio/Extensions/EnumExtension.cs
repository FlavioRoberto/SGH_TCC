using System;
using System.ComponentModel;
using System.Linq;

namespace SGH.Dominio.Services.Extensions
{
    public static class EnumExtension
    {
        public static string RetornarDescricao(this Enum value)
        {
            var campo = value.GetType().GetField(value.ToString());
            var atributos = campo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return atributos.Any() ? ((DescriptionAttribute)atributos.ElementAt(0)).Description : "Description Not Found";
        }
    }
}
