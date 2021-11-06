using AutoMapper;
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
using Microsoft.AspNetCore.Http;
using FluentValidation.AspNetCore;
using MediatR;
using System;
using SHG.Data.Contexto;
using SGH.Email.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using SGH.IoC;
using SGH.Api.Configs;

namespace SGH.APi
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning();
            services.AddPersistencia(_configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.RegistrarServicos();
            services.AddHostedService<EmailEventHandler>();
            services.AddSwaggerConfiguration();

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

            services.AddAutenticacao();

            services.AddAutoMapper(typeof(Startup));
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
            DefineCurrentCulturePtBR(app);

            UpdateDatabase(app);

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseAuthentication();
            app.UseSwaggerApplication();
            app.UseCors("MyPolicy");
            app.UseMvc();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var contexto = scope.ServiceProvider.GetRequiredService<IContexto>();
                contexto.Database.Migrate();
            }
        }

        private static void DefineCurrentCulturePtBR(IApplicationBuilder app)
        {
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
        }
    }
}
