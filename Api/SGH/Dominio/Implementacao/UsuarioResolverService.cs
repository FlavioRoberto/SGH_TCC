using Microsoft.AspNetCore.Http;
using SGH.Dominio.Contratos;
using System.Security.Claims;

namespace SGH.Dominio.Implementacao
{
    public class UsuarioResolverService : IUsuarioResolverService
    {
        private readonly IHttpContextAccessor accessor;
        public UsuarioResolverService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public string GetUser()
        {
            var userClaim = accessor?.HttpContext?.User?.Identity as ClaimsIdentity;
            return userClaim.FindFirst("codigo").Value;
        }
    }
}
