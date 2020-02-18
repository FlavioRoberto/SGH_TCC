using Aplicacao.Implementacao.Autenticacao.Comandos.AtualizarSenha;
using Microsoft.Extensions.DependencyInjection;
using SGH.Dominio.AutoMapper;
using SGH.Dominio.Contratos;
using SGH.Dominio.Implementacao;
using SGH.Dominio.Implementacao.Autenticacao.Comandos.Login;
using SGH.Dominio.Implementacao.Autenticacao.Comandos.RedefinirSenha;
using SGH.Dominio.Implementacao.Cargos.Comandos.Atualizar;
using SGH.Dominio.Implementacao.Cargos.Comandos.Criar;
using SGH.Dominio.Implementacao.Cargos.Comandos.Remover;
using SGH.Dominio.Implementacao.CargosDisciplinas.Comandos.Criar;
using SGH.Dominio.Implementacao.CargosDisciplinas.Comandos.Remover;
using SGH.Dominio.Implementacao.Curriculos.Comandos.Atualizar;
using SGH.Dominio.Implementacao.Curriculos.Comandos.Criar;
using SGH.Dominio.Implementacao.Curriculos.Comandos.Remover;
using SGH.Dominio.Implementacao.Curriculos.Consultas.ListarDisciplinas;
using SGH.Dominio.Implementacao.Cursos.Comandos.Remover;
using SGH.Dominio.Implementacao.Disciplinas.Comandos.Remover;
using SGH.Dominio.Implementacao.DIsciplinasTipoServico.Comandos.Remover;
using SGH.Dominio.Implementacao.Professores.Comandos.Atualizar;
using SGH.Dominio.Implementacao.Professores.Comandos.Criar;
using SGH.Dominio.Implementacao.Professores.Comandos.Remover;
using SGH.Dominio.Implementacao.Turnos.Comandos.Atualizar;
using SGH.Dominio.Implementacao.Turnos.Comandos.Remover;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Atualizar;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Criar;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Remover;

namespace SGH.Dominio.Extensions
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

            #region AutoMapper
            services.AddAutoMapperConfig();
            #endregion


            return services;
        }
    }
}
