using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using FluentValidation.AspNetCore;
using MediatR;
using System;
using SGH.Api.Testes.Factory;
using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SGH.IoC;
using SGH.Api.Configs;

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
            services.AddPersistenciaEmMemoria(_configuration);
            services.AddApiVersioning();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddPersistencia(_configuration);


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
            services.AddScoped<IBancoTeste<Horario>, HorarioAulaTeste>();
            services.AddScoped<IBancoTeste<Aula>, AulaBancoTeste>();
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

            services.RegistrarServicos();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyPolicy"));
            });

            services.AddAutoMapper(typeof(StartupTests));
            var assembly = AppDomain.CurrentDomain.Load("SGH.Dominio.Services");
            services.AddMediatR(assembly);

            services.AddAutenticacao();

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
