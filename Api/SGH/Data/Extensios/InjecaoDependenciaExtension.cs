using Microsoft.Extensions.DependencyInjection;
using SGH.Data.Repositorio;
using SGH.Data.Repositorio.Contratos;
using SGH.Data.Repositorio.Implementacao;
using SGH.Dominio.Core.Model;

namespace SGH.Data.Extensios
{
    public static class InjecaoDependenciaExtension
    {
        public static IServiceCollection AddPersistencia(this IServiceCollection services)
        {
            services.AddScoped<IRepositorio<Curso>, CursoRepositorio>();
            services.AddScoped<ICurriculoRepositorio, CurriculoRepositorio>();
            services.AddScoped<IDisciplinaRepositorio, DisciplinaRepositorio>();
            services.AddScoped<IDisciplinaTipoRepositorio, DisciplinaTipoRepositorio>();
            services.AddScoped<IUsuarioPerfilRepositorio, UsuarioPerfilRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IProfessorRepositorio, ProfessorRepositorio>();
            services.AddScoped<ICursoRepositorio, CursoRepositorio>();

            return services;
        }
}

