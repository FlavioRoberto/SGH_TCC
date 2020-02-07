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
using SGH.Api.Filters;
using SGH.Data.Extensios;
using SGH.Dominio.Extensions;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

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
                options.UseInMemoryDatabase(connectionString);
            });

            services.AddApiVersioning();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDominioCore(_configuration.GetSection("ConfiguracoesEmail"));
            services.AddPersistencia();
            services.AddDominio();

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
            var assembly = AppDomain.CurrentDomain.Load("SGH.Dominio");
            services.AddMediatR(assembly);

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add(typeof(FiltroExcecaoAtributo));
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

            var context = app.ApplicationServices.GetService<IContexto>();
            AdicionarDadosTeste(context);

            app.UseAuthentication();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }

        private static void AdicionarDadosTeste(IContexto contexto)
        {
            var perfis = new List<UsuarioPerfil>
            {
              new UsuarioPerfil
              {
                  Administrador = true,
                  Descricao = "Administrador"
              }
            };

            contexto.UsuarioPerfil.AddRange(perfis);
            contexto.SaveChanges();

            var usuarios = new List<Usuario>
            {
                new Usuario {
                   Ativo = true,
                   Email = "admin@gmail.com",
                   Login = "admin",
                   Nome = "administrador",
                   PerfilCodigo = 1,
                   Senha = "admin".ToMD5(),
                   Telefone = "3732153995"
                }
            };

            contexto.Usuario.AddRange(usuarios);
            contexto.SaveChanges();
        }
    }
}
