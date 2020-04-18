using Microsoft.Extensions.DependencyInjection;
using SGH.Data.Store;
using System.Text;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SGH.Dominio.Services.Extensions
{
    public static class AutenticacaoServiceExtension
    {
        public static IServiceCollection AdicionarAutenticacao(this IServiceCollection services)
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
                options.AddPolicy("admin", policy => policy.RequireClaim("admin", "administrador"));
                options.AddPolicy("pedagogico", policy => policy.RequireClaim("perfilId", "2"));
                options.AddPolicy("todos", policy => policy.RequireAssertion(context =>
                        context.User.HasClaim(c =>
                            (c.Type == "perfilId" && (Convert.ToInt32(c.Value) > 0)))));
            });

            return services;
        }
    }
}
