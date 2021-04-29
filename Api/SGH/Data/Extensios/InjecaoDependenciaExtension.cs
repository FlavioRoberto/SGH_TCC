using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGH.Data.Repositorio;
using SGH.Data.Repositorio.Contratos;
using SGH.Data.Repositorio.Implementacao;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System;

namespace SGH.Data.Extensios
{
    public static class InjecaoDependenciaExtension
    {
        public static IServiceCollection AddPersistencia(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["CONNECTION_STRING"];

            services.AddScoped<IContexto, Contexto>();

            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<IContexto, Contexto>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped(typeof(IRepositorio<>),typeof(RepositorioBase<>));

            services.AddScoped<IRepositorio<Curso>, CursoRepositorio>();
            services.AddScoped<ICurriculoRepositorio, CurriculoRepositorio>();
            services.AddScoped<IDisciplinaRepositorio, DisciplinaRepositorio>();
            services.AddScoped<IDisciplinaTipoRepositorio, DisciplinaTipoRepositorio>();
            services.AddScoped<IUsuarioPerfilRepositorio, UsuarioPerfilRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IProfessorRepositorio, ProfessorRepositorio>();
            services.AddScoped<ICursoRepositorio, CursoRepositorio>();
            services.AddScoped<ITurnoRepositorio, TurnoRepositorio>();
            services.AddScoped<ICargoRepositorio, CargoRepositorio>();
            services.AddScoped<ICargoDisciplinaRepositorio, CargoDisciplinaRepositorio>();
            services.AddScoped<ICurriculoDisciplinaRepositorio, CurriculoDisciplinaRepositorio>();
            services.AddScoped<IBlocoRepositorio, BlocoRepositorio>();
            services.AddScoped<ISalaRepositorio, SalaRepositorio>();
            services.AddScoped<IHorarioAulaRepositorio, HorarioAulaRepositorio>();
            services.AddScoped<IAulaRepositorio, AulaRepositorio>();

            return services;
        }

        public static IServiceCollection AddPersistenciaEmMemoria(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:SghSqlConnections"];
            services.AddDbContext<Contexto>(options =>
                    {
                        options.UseInMemoryDatabase(connectionString)
                               .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                    });

            return services;
        }
    }
}


