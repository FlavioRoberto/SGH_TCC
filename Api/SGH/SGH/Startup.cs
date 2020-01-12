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
using Microsoft.Extensions.Logging;
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
using SGH.Api.Servicos.Email;
using SGH.Dominio.Extensions;

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


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnections:ConexaoLocal"];
            services.Configure<EmailSettings>(_configuration.GetSection("ConfiguracoesEmail"));
            services.AddTransient<IEmailService, EmailService>();

            services.AddDbContext<MySqlContext>(options =>
            {
                options.UseMySQL(connectionString);
            });

            services.AddApiVersioning();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

            services.AddAutoMapper();
            var assembly = AppDomain.CurrentDomain.Load("Aplicacao");
            services.AddMediatR(assembly);

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
