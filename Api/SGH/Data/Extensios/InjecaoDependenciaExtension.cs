using Microsoft.Extensions.DependencyInjection;
using SGH.Data.Repositorio;
using SGH.Data.Repositorio.Contratos;
using SGH.Data.Repositorio.Implementacao;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;

namespace SGH.Data.Extensios
{
    public static class InjecaoDependenciaExtension
    {
        public static IServiceCollection AddPersistencia(this IServiceCollection services)
        {
            services.AddScoped<IContexto, MySqlContext>();
            services.AddScoped<IRepositorio<Curso>, CursoRepositorio>();
            services.AddScoped<ICurriculoRepositorio, CurriculoRepositorio>();
            services.AddScoped<IDisciplinaRepositorio, DisciplinaRepositorio>();
            services.AddScoped<IDisciplinaTipoRepositorio, DisciplinaTipoRepositorio>();
            services.AddScoped<IUsuarioPerfilRepositorio, UsuarioPerfilRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IProfessorRepositorio, ProfessorRepositorio>();
            services.AddScoped<ICursoRepositorio, CursoRepositorio>();
            services.AddScoped<ITurnoRepositorio, TurnoRepositorio>();
            services.AddScoped<IRepositorio<Cargo>, RepositorioBase<Cargo>>();
            services.AddScoped<ICargoRepositorio, CargoRepositorio>();
            services.AddScoped<IRepositorio<CargoDisciplina>, RepositorioBase<CargoDisciplina>>();
            services.AddScoped<ICargoDisciplinaRepositorio, CargoDisciplinaRepositorio>();
            services.AddScoped<ICurriculoDisciplinaRepositorio, CurriculoDisciplinaRepositorio>();
            services.AddScoped<IRepositorio<Curriculo>, RepositorioBase<Curriculo>>();
            services.AddScoped<IRepositorio<CurriculoDisciplina>, RepositorioBase<CurriculoDisciplina>>();
            services.AddScoped<IRepositorio<Bloco>, RepositorioBase<Bloco>>();
            services.AddScoped<IBlocoRepositorio, BlocoRepositorio>();

            return services;
        }
    }
}


