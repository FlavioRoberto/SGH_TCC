using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SGH.Dominio.Services.AutoMapper
{
    public static class AutoMapperService
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            var autoMapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<CargoProfile>();
                config.AddProfile<CurriculoProfile>();
                config.AddProfile<CursoProfile>();
                config.AddProfile<DisciplinaProfile>();
                config.AddProfile<DisciplinaTipoProfile>();
                config.AddProfile<ProfessorProfile>();
                config.AddProfile<TurnoProfile>();
                config.AddProfile<UsuarioProfile>();
                config.AddProfile<CargoDisciplinaProfile>();
            });

            IMapper mapper = autoMapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
