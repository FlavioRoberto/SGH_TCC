using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGH.Dominio.Services.AutoMapper;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Email;
using SGH.Dominio.Services.Implementacao;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Aulas.Consulta.ListarPorHorario;
using SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.AtualizarSenha;
using SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.Login;
using SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.RedefinirSenha;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Editar;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarPorCurriculo;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarTodas;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Editar;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Consultas.ListarDisciplinas;
using SGH.Dominio.Services.Implementacao.Cursos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Cursos.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Cursos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Disciplinas.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.DIsciplinasTipoServico.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Relatorios.Consultas.HorarioGeral;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Remover;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Remover;
using SGH.Dominio.Services.Servicos;
using SGH.Relatorios;

namespace SGH.Dominio.Services.Extensions
{
    public static class InjecaoDependenciaExtension
    {
        public static IServiceCollection AddDominio(this IServiceCollection services, IConfigurationSection configuracaoSecao)
        {
            services.Configure<EmailConfiguracoes>(configuracaoSecao);
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICargoService, CargoService>();

            services.AddScoped<IValidador<AtualizarCurriculoComando>, AtualizarCurriculoComandoValidador>();
            services.AddScoped<IValidador<AtualizarSenhaComando>, AtualizarSenhaComandoValidador>();
            services.AddScoped<IValidador<CriarUsuarioComando>, CriarUsuarioComandoValidador>();
            services.AddScoped<IValidador<AtualizarUsuarioComando>, AtualizarUsuarioComandoValidador>();            
            services.AddScoped<IValidador<LoginComando>, LoginComandoValidator>();
            services.AddScoped<IValidador<RedefinirSenhaComando>, RedefinirSenhaComandoValidador>();
            services.AddScoped<IValidador<RemoverCurriculoComando>, RemoverCurriculoComandoValidador>();
            services.AddScoped<IValidador<RemoverDisciplinaComando>, RemoverDisciplinaComandoValidador>();
            services.AddScoped<IValidador<RemoverDisciplinaTipoComando>, RemoverDisciplinaTipoComandoValidador>();
            services.AddScoped<IValidador<RemoverUsuarioComando>, RemoverUsuarioComandoValidador>();
            services.AddTransient<IUsuarioResolverService, UsuarioResolverService>();
            services.AddScoped<IValidador<RemoverCursoComando>, RemoverCursoComandoValidador>();
            services.AddScoped<IValidador<CriarProfessorComando>, CriarProfessorComandoValidador>();
            services.AddScoped<IValidador<AtualizarProfessorComando>, AtualizarProfessorComandoValidador>();
            services.AddScoped<IValidador<RemoverProfessorComando>, RemoverProfessorComandoValidador>();
            services.AddScoped<IValidador<AtualizarTurnoComando>, AtualizarTurnoComandoValidador>();
            services.AddScoped<IValidador<RemoverTurnoComando>, RemoverTurnoComandoValidador>();
            services.AddScoped<IValidador<CriarCurriculoComando>, CriarCurriculoComandoValidador>();
            services.AddScoped<IValidador<CriarCargoComando>, CriarCargoComandoValidador>();
            services.AddScoped<IValidador<RemoverCargoComando>, RemoverCargoComandoValidador>();
            services.AddScoped<IValidador<AtualizarCargoComando>, AtualizarCargoComandoValidador>();
            services.AddScoped<IValidador<CriarCargoDisciplinaComando>, CriarCargoDisciplinaComandoValidador>();
            services.AddScoped<IValidador<RemoverCargoDisciplinaComando>, RemoverCargoDisciplinaComandoValidador>();
            services.AddScoped<IValidador<ListarTodasDisciplinasCargoConsulta>, ListarTodasDisciplinasCargoConsultaValidador>();
            services.AddScoped<IValidador<CriarCurriculoDisciplinaComando>, CriarCurriculoDisciplinaComandoValidador>();
            services.AddScoped<IValidador<RemoverCurriculoDisciplinaComando>, RemoverCurriculoDisciplinaComandoValidador>();
            services.AddScoped<IValidador<ListarDisciplinasCurriculoConsulta>, ListarDisciplinaCurriculoConsultaValidador>();
            services.AddScoped<IValidador<EditarCurriculoDisciplinaComando>, EditarCurriculoDisciplinaComandoValidador>();
            services.AddScoped<IValidador<CriarBlocoComando>, CriarBlocoComandoValidador>();
            services.AddScoped<IValidador<AtualizarBlocoComando>, AtualizarBlocoComandoValidador>();
            services.AddScoped<IValidador<RemoverBlocoComando>, RemoverBlocoComandoValidador>();
            services.AddScoped<IValidador<CriarSalaComando>, CriarSalaComandoValidador>();
            services.AddScoped<IValidador<AtualizarSalaComando>, AtualizarSalaComandoValidador>();
            services.AddScoped<IValidador<RemoverSalaComando>, RemoverSalaComandoValidador>();
            services.AddScoped<IValidador<CriarHorarioAulaComando>, CriarHorarioAulaComandoValidador>();
            services.AddScoped<IValidador<RemoverHorarioComando>, RemoverHorarioComandoValidador>();
            services.AddScoped<IValidador<AtualizarHorarioAulaComando>, AtualizarHorarioAulaComandoValidador>();
            services.AddScoped<IValidador<ListarDisciplinaCargoPorCurriculoConsulta>, ListarDisciplinasCargoPorCurriculoConsultaValidador>();
            services.AddScoped<IValidador<CriarAulaComando>, CriarAulaComandoValidador>();
            services.AddScoped<IValidador<ListarAulaPorHorarioConsulta>, ListarAulaPorHorarioValidador>();
            services.AddScoped<IValidador<RemoverAulaComando>, RemoverAulaComandoValidador>();
            services.AddScoped<IValidador<EditarCargoDisciplinaComando>, EditarCargoDisciplinaComandoValidador>();
            services.AddScoped<IValidador<GerarHorarioGeralRelatorioConsulta>, GerarRelatorioHorarioGeralConsultaValidador>();
            services.AddTransient<IValidador<CriarCursoComando>, CriarCursoComandoValidador>();
            services.AddTransient<IValidador<AtualizarCursoComando>, AtualizarCursoComandoValidador>();

            #region AutoMapper
            services.AddAutoMapperConfig();
            #endregion

            #region Relatorio
            services.AddRelatorio(configuracaoSecao);
            #endregion

            return services;
        }
    }
}
