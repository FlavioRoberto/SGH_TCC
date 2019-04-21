using Data.Contexto;
using Dominio.Model;
using Dominio.Model.CurriculoModel;
using Dominio.Model.DisciplinaModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repositorio;
using Repositorio.Implementacao;
using Repositorio.Implementacao.Curriculo;
using Repositorio.Implementacao.CurriculoImplementacao;
using Repositorio.Implementacao.Disciplina;

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
            services.AddScoped<IRepositorio<Curriculo>, CurriculoRepositorio>();
            services.AddScoped<IRepositorio<CurriculoDisciplina>, CurriculoDisciplinaRepositorio>();
            services.AddScoped<IRepositorio<CurriculoDisciplinaPreRequisito>, CurDisPreRequisitoRepositorio>();
            services.AddScoped<IRepositorio<Disciplina>, DisciplinaRepositorio>();
            services.AddScoped<IRepositorio<DisciplinaTipo>, DisciplinaTipoRepositorio>();
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

            services.AddMvc();

        }

        // This method gets called by the runtime. Use sthis method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors("MyPolicy");
            app.UseMvc();
        }
    }
}
