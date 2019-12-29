using System;

namespace SGH.Dominio.Core.Extensions
{
    public static class StringExtension
    {
        public static bool IgualA(this string value, string campoComparar)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(campoComparar))
                return false;

            return value.ToLower().Trim().Equals(campoComparar.Trim().ToLower());
        }

        public static int ToInt(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return 0;

                return int.Parse(value);

            }catch(Exception e)
            {
                throw e;
            }
        }
    }
}
