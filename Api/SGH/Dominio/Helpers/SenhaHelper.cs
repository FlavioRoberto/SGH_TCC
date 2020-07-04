using System;
using System.Text;

namespace SGH.Dominio.Services.Helpers
{
    public static class SenhaHelper
    {
        public static string Gerar()
        {
            string codigoSenha = $"{DateTime.Now.Ticks}{new Guid()}";
            return BitConverter.ToString(new System.Security.Cryptography.SHA512CryptoServiceProvider()
                .ComputeHash(Encoding.Default.GetBytes(codigoSenha))).Replace("-", String.Empty).Substring(0, 8);
        }
    }
}
