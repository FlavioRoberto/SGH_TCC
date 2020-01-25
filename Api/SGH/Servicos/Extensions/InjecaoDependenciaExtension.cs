
using SGH.Dominio.Core.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SGH.Dominio.Core.Extensions
{
    public static class InjecaoDependenciaExtension
    {
        public static IServiceCollection AddDominioCore(this IServiceCollection services, IConfigurationSection configuracaoSecao)
        {
            services.Configure<EmailConfiguracoes>(configuracaoSecao);
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
