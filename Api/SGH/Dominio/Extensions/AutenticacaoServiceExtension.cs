using Microsoft.Extensions.DependencyInjection;

namespace SGH.Dominio.Services.Extensions
{
    public static class AutenticacaoServiceExtension
    {

        public static IServiceCollection AddAutenticacao(this IServiceCollection services)
        {
            services.AdicionarAutenticacao();
            return services;
        }

    }
}
