using AutoMapper;
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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using FluentValidation.AspNetCore;
using MediatR;
using System;
using SHG.Data.Contexto;
using SGH.Dominio.Core.Store;
using SGH.Dominio.Core.Extensions;
using SGH.Data.Extensios;
using SGH.Dominio.Services.Extensions;
using SGH.Api.Testes.Factory;
using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Shared.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SGH.Dominio.Core.Model;

namespace SGH.APi
{
    public class StartupTests
    {
        private IConfiguration _configuration { get; }

        public IHostingEnvironment environment { get; }

        public StartupTests(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnections:ConexaoLocal"];
            services.AddDbContext<MySqlContext>(options =>
            {
                options.UseInMemoryDatabase(connectionString)
                       .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.AddApiVersioning();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDominioCore(_configuration.GetSection("ConfiguracoesEmail"));
            services.AddPersistencia();
            services.AddDominio();

            #region FAKE_DB
            services.AddScoped<IBancoTesteFactory, BancoTesteFactory>();
            services.AddScoped<IBancoTeste<UsuarioPerfil>, UsuarioPerfilBancoTeste>();
            services.AddScoped<IBancoTeste<Usuario>, UsuarioBancoTeste>();
            services.AddScoped<IBancoTeste<Professor>, ProfessorBancoTeste>();
            services.AddScoped<IBancoTeste<DisciplinaTipo>, TipoDisciplinaBancoTeste>();
            services.AddScoped<IBancoTeste<Disciplina>, DisciplinaBancoTeste>();
            services.AddScoped<IBancoTeste<Curso>, CursoBancoTeste>();
            services.AddScoped<IBancoTeste<Curriculo>, CurriculoBancoTeste>();
            services.AddScoped<IBancoTeste<Cargo>, CargoBancoTeste>();
            services.AddScoped<IBancoTeste<Turno>, TurnoBancoTeste>();
            services.AddScoped<IBancoTeste<Bloco>, BlocoBancoTeste>();
            services.AddScoped<IBancoTeste<Sala>, SalaBancoTeste>();
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

            services.AddAutoMapper(typeof(StartupTests));
            var assembly = AppDomain.CurrentDomain.Load("SGH.Dominio.Services");
            services.AddMediatR(assembly);

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
             .AddJsonOptions(options =>
             {
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
             })
             .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<Startup>())
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            var factoryDb = app.ApplicationServices.GetService<IBancoTesteFactory>();
            
            factoryDb.InicializarBanco();

            app.UseAuthentication();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
        
}
