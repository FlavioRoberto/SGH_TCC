using Dominio.Model.Autenticacao;
using Microsoft.IdentityModel.Tokens;
using Servico.Store;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Servico.Extensions
{
    public static class TokenGeradorHelper
    {
        public static string Gerar(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuracoes.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("nome", usuario.Nome),
                    new Claim("codigo", usuario.Codigo.ToString()),
                    new Claim("perfilId", usuario.PerfilCodigo.ToString()),
                    new Claim("login", usuario.Login),
                    new Claim("foto", string.Empty)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
