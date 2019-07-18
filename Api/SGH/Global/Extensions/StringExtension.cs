namespace Global.Extensions
{
    public static class StringExtension
    {
        public static bool IgualA(this string value, string campoComparar)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(campoComparar))
                return false;

            return value.ToLower().Trim().Equals(campoComparar.Trim().ToLower());
        }
    }
}
