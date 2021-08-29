using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SGH.Dominio.Services.Store;
using System;
using System.Text;

namespace SGH.Api.Configs
{
    public static class AutenticacaoServiceExtension
    {

        public static IServiceCollection AddAutenticacao(this IServiceCollection services)
        {
            services.AdicionarAutenticacao();
            return services;
        }

        private static IServiceCollection AdicionarAutenticacao(this IServiceCollection services)
        {
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
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("user", policy => policy.RequireClaim("perfilId", "5"));
                options.AddPolicy("pedagogico", policy => policy.RequireClaim("perfilId", "2"));
                options.AddPolicy("admin", policy => policy.RequireClaim("admin", "administrador"));
                options.AddPolicy("coordenacao", policy => policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                            (c.Type == "perfilId" && (Convert.ToInt32(c.Value) == 3) ||
                            (c.Type == "admin" && c.Value == "administrador")
                        ))
                ));

                options.AddPolicy("infraestrutura", policy => policy.RequireAssertion(context =>
                       context.User.IsInRole("Administrador") ||
                       context.User.IsInRole("infraestrutura")));

                options.AddPolicy("todos", policy => policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                            (c.Type == "perfilId" && (Convert.ToInt32(c.Value) > 0)))));
            });

            return services;
        }
    }
}
