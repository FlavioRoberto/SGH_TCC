using Microsoft.Extensions.DependencyInjection;
using SGH.Dominio.Services.AutoMapper;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao;
using SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.AtualizarSenha;
using SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.Login;
using SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.RedefinirSenha;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarTodas;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Curriculos.Consultas.ListarDisciplinas;
using SGH.Dominio.Services.Implementacao.Cursos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Disciplinas.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Remover;

namespace SGH.Dominio.Services.Extensions
{
    public static class InjecaoDependenciaExtension
    {
        public static IServiceCollection AddDominio(this IServiceCollection services)
        {
            services.AddScoped<IAtualizarCurriculoComandoValidador, AtualizarCurriculoComandoValidador>();
            services.AddScoped<IAtualizarSenhaComandoValidador, AtualizarSenhaComandoValidador>();
            services.AddScoped<IAtualizarCurriculoComandoValidador, AtualizarCurriculoComandoValidador>();
            services.AddScoped<ICriarUsuarioComandoValidador, CriarUsuarioComandoValidador>();
            services.AddScoped<IAtualizarUsuarioComandoValidador, AtualizarUsuarioComandoValidador>();            
            services.AddScoped<IListarDisciplinaCurriculoConsultaValidador, ListarDisciplinaCurriculoConsultaValidador>();
            services.AddScoped<ILoginComandoValidator, LoginComandoValidator>();
            services.AddScoped<IRedefinirSenhaComandoValidador, RedefinirSenhaComandoValidador>();
            services.AddScoped<IRemoverCurriculoComandoValidador, RemoverCurriculoComandoValidador>();
            services.AddScoped<IRemoverDisciplinaComandoValidador, RemoverDisciplinaComandoValidador>();
            services.AddScoped<IRemoverDisciplinaTipoComandoValidador, RemoverDisciplinaTipoComandoValidador>();
            services.AddScoped<IRemoverUsuarioComandoValidador, RemoverUsuarioComandoValidador>();
            services.AddTransient<IUsuarioResolverService, UsuarioResolverService>();
            services.AddScoped<IRemoverCursoComandoValidador, RemoverCursoComandoValidador>();
            services.AddScoped<ICriarProfessorComandoValidador, CriarProfessorComandoValidador>();
            services.AddScoped<IAtualizarProfessorComandoValidador, AtualizarProfessorComandoValidador>();
            services.AddScoped<IRemoverProfessorComandoValidador, RemoverProfessorComandoValidador>();
            services.AddScoped<IAtualizarTurnoComandoValidador, AtualizarTurnoComandoValidador>();
            services.AddScoped<IRemoverTurnoComandoValidador, RemoverTurnoComandoValidador>();
            services.AddScoped<ICriarCurriculoComandoValidador, CriarCurriculoComandoValidador>();
            services.AddScoped<ICriarCargoComandoValidador, CriarCargoComandoValidador>();
            services.AddScoped<IRemoverCargoComandoValidador, RemoverCargoComandoValidador>();
            services.AddScoped<IAtualizarCargoComandoValidador, AtualizarCargoComandoValidador>();
            services.AddScoped<ICriarCargoDisciplinaComandoValidador, CriarCargoDisciplinaComandoValidador>();
            services.AddScoped<IRemoverCargoDisciplinaComandoValidador, RemoverCargoDisciplinaComandoValidador>();
            services.AddScoped<IListarTodasDisciplinasCargoConsultaValidador, ListarTodasDisciplinasCargoConsultaValidador>();

            #region AutoMapper
            services.AddAutoMapperConfig();
            #endregion


            return services;
        }
    }
}
