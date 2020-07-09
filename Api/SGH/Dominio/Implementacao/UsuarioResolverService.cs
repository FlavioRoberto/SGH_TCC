using Microsoft.AspNetCore.Http;
using SGH.Dominio.Services.Contratos;
using System;
using System.Security.Claims;

namespace SGH.Dominio.Services.Implementacao
{
    public class UsuarioResolverService : IUsuarioResolverService
    {
        private readonly IHttpContextAccessor accessor;
        public UsuarioResolverService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public int RetornarCodigoUsuario()
        {
            var userClaim = accessor?.HttpContext?.User?.Identity as ClaimsIdentity;
            return Convert.ToInt32(userClaim.FindFirst("codigo").Value);
        }
    }
}
