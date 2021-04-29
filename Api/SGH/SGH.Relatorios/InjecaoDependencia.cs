using Microsoft.Extensions.DependencyInjection;
using SGH.Relatorios.Contratos;
using SGH.Relatorios.Implementacoes;

namespace SGH.Relatorios
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AddRelatorio(this IServiceCollection services)
        {
            services.AddTransient<IRelatorioServico, RelatorioServico>();
            return services;
        }
    }
}
