using Data.Contexto;
using Dominio.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repositorio;
using Repositorio.Implementacao;

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


            services.AddMvc();

            services.AddApiVersioning();

            //Injeção de dependências dos servicos
            services.AddScoped<IRepositorio<Turno>, TurnoRepositorio>();
            services.AddScoped<IRepositorio<Curso>, CursoRepositorio>();
          
        }

        // This method gets called by the runtime. Use sthis method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
