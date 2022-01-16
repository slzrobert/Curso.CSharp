using Curso.CSharp.Repository;
using Curso.CSharp.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Curso.CSharp.Api
{
    public class Startup
    {
        private readonly MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Este m�todo � chamado pelo tempo de execu��o. Use este m�todo para adicionar servi�os ao cont�iner.
        // Para obter mais informa��es sobre como configurar seu aplicativo, visite https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<CarroService>();
            services.AddScoped<CarroRepository>();
            
            services.AddDbContext<CursoDBContexto>(opt => 
            {
                opt
                .UseMySql(Configuration["Conexao"], serverVersion)
                //As tr�s op��es a seguir ajudam na depura��o, mas devem
                //ser alteradas ou removidas para produ��o.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            });

            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvcCore();
            services.AddHttpContextAccessor();
        }

        // Este m�todo � chamado pelo tempo de execu��o. Use este m�todo para configurar o pipeline de solicita��o HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
