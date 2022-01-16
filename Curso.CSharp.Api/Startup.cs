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

        // Este método é chamado pelo tempo de execução. Use este método para adicionar serviços ao contêiner.
        // Para obter mais informações sobre como configurar seu aplicativo, visite https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<CarroService>();
            services.AddScoped<CarroRepository>();
            
            services.AddDbContext<CursoDBContexto>(opt => 
            {
                opt
                .UseMySql(Configuration["Conexao"], serverVersion)
                //As três opções a seguir ajudam na depuração, mas devem
                //ser alteradas ou removidas para produção.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            });

            services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvcCore();
            services.AddHttpContextAccessor();
        }

        // Este método é chamado pelo tempo de execução. Use este método para configurar o pipeline de solicitação HTTP.
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
