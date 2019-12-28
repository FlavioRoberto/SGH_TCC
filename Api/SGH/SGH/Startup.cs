using AutoMapper;
using Data.Contexto;
using Dominio.Model;
using Dominio.Model.Autenticacao;
using Dominio.Model.DisciplinaModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repositorio;
using Repositorio.Contratos;
using Repositorio.Implementacao;
using Repositorio.Implementacao.Autenticacao;
using Repositorio.Implementacao.CurriculoImplementacao;
using Repositorio.Implementacao.Disciplina;
using Servico.Contratos;
using Servico.Store;
using System.Text;
using Global.Extensions;
using Servico.Implementacao;
using Microsoft.AspNetCore.Http;
using Api.Servicos.Email;
using Servico.Implementacao.DisciplinaImp;
using Servico.Implementacao.CurriculoImp;
using Servico.Implementacao.Autenticacao.Comandos.Login;
using FluentValidation.AspNetCore;
using Api.Filters;
using Servico.Implementacao.Autenticacao.Contratos;
using Servico.Implementacao.Usuarios;
using Servico.Implementacao.Autenticacao.Comandos.RedefinirSenha;
using Servico.Implementacao.Autenticacao.Comandos.AtualizarSenha;

namespace Api
{
    public class Startup
    {
        private readonly ILogger _logger;
        private IConfiguration _configuration { get; }
        public IHostingEnvironment environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            this.environment = environment;
            _logger = logger;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnections:ConexaoLocal"];

            services.AddDbContext<MySqlContext>(options =>
            {
                options.UseMySQL(connectionString);
            });

            services.AddApiVersioning();

            //Injeção de dependências dos servicos
            #region Periodizacao
            services.AddScoped<IRepositorio<Turno>, TurnoRepositorio>();
            services.AddScoped<IRepositorio<Curso>, CursoRepositorio>();
            services.AddScoped<ICurriculoRepositorio, CurriculoRepositorio>();
            services.AddScoped<IRepositorio<Disciplina>, DisciplinaRepositorio>();
            services.AddScoped<IRepositorio<DisciplinaTipo>, DisciplinaTipoRepositorio>();
            services.AddScoped<IRepositorio<UsuarioPerfil>, UsuarioPerfilRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IProfessorRepositorio, ProfessorRepositorio>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUsuarioResolverService, UsuarioResolverService>();
            services.AddScoped<IUsuarioPerfilService, UsuarioPerfilServico>();
            services.AddScoped<IUsuarioService, UsuarioServico>();
            services.AddScoped<IDisciplinaService, DisciplinaServico>();
            services.AddScoped<ICursoService, CursoServico>();
            services.AddScoped<ICurriculoService, CurriculoServico>();
            services.AddScoped<IProfessorService, ProfessorServico>();

            services.AddScoped<IAutenticacaoService, LoginComandoHandler>();
            services.AddScoped<ILoginComandoValidator, LoginComandoValidator>();

            services.AddScoped<IRedefinirSenhaComandoValidador, RedefinirSenhaComandoValidador>();
            services.AddScoped<IRedefinirSenhaService, RedefinirSenhaComandoHandler>();

            services.AddScoped<IAtualizarSenhaComandoValidador, AtualizarSenhaComandoValidador>();
            services.AddScoped<IAtualizarSenhaService, AtualizarSenhaComandoHandler>();

            services.Configure<EmailSettings>(_configuration.GetSection("ConfiguracoesEmail"));
            services.AddTransient<IEmailService, EmailService>();
            #endregion

            services.AddCors(o =>
                o.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                })
           );


            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });

            var key = Encoding.ASCII.GetBytes(Configuracoes.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }); ;

            services.AddAuthorization(options =>
            {
                options.AddPolicy("user", policy => policy.RequireClaim("perfilId", "5"));
                options.AddPolicy("admin", policy => policy.RequireClaim("admin", "administrador"));
                options.AddPolicy("pedagogico", policy => policy.RequireClaim("perfilId", "2"));
                options.AddPolicy("todos", policy => policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                            (c.Type == "perfilId" && (c.Value.ToInt() > 0)))));
            });

            services.AddAutoMapper();

            services.AddMvc(config =>
             {
                 var policy = new AuthorizationPolicyBuilder()
                                  .RequireAuthenticatedUser()
                                  .Build();
                 config.Filters.Add(new AuthorizeFilter(policy));
                 config.Filters.Add(typeof(FiltroExcecaoAtributo));
             })
             .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<Startup>())
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}
