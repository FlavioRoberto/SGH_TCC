using Microsoft.AspNetCore.Http;
using Servico.Contratos;
using System.Security.Claims;

namespace Servico.Implementacao
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
