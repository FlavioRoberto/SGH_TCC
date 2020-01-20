using Aplicacao.Implementacao.Autenticacao.Comandos.AtualizarSenha;
using Aplicacao.Implementacao.Autenticacao.Comandos.Login;
using Aplicacao.Implementacao.Autenticacao.Comandos.RedefinirSenha;
using Microsoft.Extensions.DependencyInjection;
using SGH.Dominio.Contratos;
using SGH.Dominio.Implementacao;
using SGH.Dominio.Implementacao.Curriculos.Comandos.Atualizar;
using SGH.Dominio.Implementacao.Curriculos.Comandos.Remover;
using SGH.Dominio.Implementacao.Curriculos.Consultas.ListarDisciplinas;
using SGH.Dominio.Implementacao.Cursos.Comandos.Remover;
using SGH.Dominio.Implementacao.Disciplinas.Comandos.Remover;
using SGH.Dominio.Implementacao.DIsciplinasTipoServico.Comandos.Remover;
using SGH.Dominio.Implementacao.Professores.Comandos.Atualizar;
using SGH.Dominio.Implementacao.Professores.Comandos.Criar;
using SGH.Dominio.Implementacao.Professores.Comandos.Remover;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Criar;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Remover;

namespace SGH.Dominio.Extensions
{
    public static class InjecaoDependenciaExtension
    {
        public static IServiceCollection AddDominio(this IServiceCollection services)
        {
            //Injeção de dependências dos servicos
            #region Periodizacao
            services.AddScoped<IAtualizarCurriculoComandoValidador, AtualizarCurriculoComandoValidador>();
            services.AddScoped<IAtualizarSenhaComandoValidador, AtualizarSenhaComandoValidador>();
            services.AddScoped<IAtualizarCurriculoComandoValidador, AtualizarCurriculoComandoValidador>();
            services.AddScoped<ICriarUsuarioComandoValidador, CriarUsuarioComandoValidador>();
            services.AddScoped<ICriarUsuarioComandoValidador, CriarUsuarioComandoValidador>();
            services.AddScoped<IListarDisciplinaCurriculoConsultaValidador, ListarDisciplinaCurriculoConsultaValidador>();
            services.AddScoped<ILoginComandoValidator, LoginComandoValidator>();
            services.AddScoped<IRedefinirSenhaComandoValidador, RedefinirSenhaComandoValidador>();
            services.AddScoped<IRemoverCurriculoComandoValidador, RemoverCurriculoComandoValidador>();
            services.AddScoped<IRemoverDisciplinaComandoValidador, RemoverDisciplinaComandoValidador>();
            services.AddScoped<IRemoverDisciplinaTipoComandoValidador, RemoverDisciplinaTipoComandoValidador>();
            services.AddScoped<IRemoverUsuarioComandoValidador, RemoverUsuarioComandoValidador>();
            services.AddScoped<IUsuarioComandoValidador, IUsuarioComandoValidador>();
            services.AddTransient<IUsuarioResolverService, UsuarioResolverService>();
            services.AddScoped<IRemoverCursoComandoValidador, RemoverCursoComandoValidador>();
            services.AddScoped<ICriarProfessorComandoValidador, CriarProfessorComandoValidador>();
            services.AddScoped<IAtualizarProfessorComandoValidador, AtualizarProfessorComandoValidador>();
            services.AddScoped<IRemoverProfessorComandoValidador, RemoverProfessorComandoValidador>();
            #endregion

            return services;
        }
    }
}
