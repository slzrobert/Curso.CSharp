using Curso.CSharp.Api.Extensions;
using Curso.CSharp.Models.Interfaces.Repository;
using Curso.CSharp.Models.Interfaces.Service;
using Curso.CSharp.Repository;
using Curso.CSharp.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;

namespace Curso.CSharp.Api
{
    public class Startup
    {
        private readonly MySqlServerVersion _serverVersion = new(new Version(8, 0, 27));

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Este m�todo � chamado pelo tempo de execu��o. Use este m�todo para adicionar servi�os ao cont�iner.
        // Para obter mais informa��es sobre como configurar seu aplicativo, visite https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjectionConfiguration(services);
            
            services.AddLogging();
            
            services.AddDbContext<CursoDbContexto>(opt => 
            {
                opt
                .UseMySql(_configuration["Conexao"], _serverVersion)
                //As tr�s op��es a seguir ajudam na depura��o, mas devem
                //ser alteradas ou removidas para produ��o.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            });

            /*
             services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            */

            services.AddAuthorization();
            
            services.AddMvcCore()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                })
                .AddApiExplorer();

            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Curso C# - v1",
                    Description = "Um exemplo de Web API ASP.NET Core.",
                    Contact = new OpenApiContact
                    {
                        Name = "Robert Pereira",
                        Email = "slzrobert@hotmail.com",
                        Url = new Uri("https://github.com/slzrobert")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GPL-3.0 License",
                        Url = new Uri("https://github.com/slzrobert/Curso.CSharp/blob/master/LICENSE")
                    }
                });

                opt.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Curso C# - v2",
                    Description = "Um exemplo de Web API ASP.NET Core.",
                    Contact = new OpenApiContact
                    {
                        Name = "Robert Pereira",
                        Email = "slzrobert@hotmail.com",
                        Url = new Uri("https://github.com/slzrobert")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GPL-3.0 License",
                        Url = new Uri("https://github.com/slzrobert/Curso.CSharp/blob/master/LICENSE")
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                opt.IncludeXmlComments(XmlCommentsFilePath);
            });
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHttpContextAccessor();
        }

        // Este m�todo � chamado pelo tempo de execu��o. Use este m�todo para configurar o pipeline de solicita��o HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureCustomExceptionMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger(opt =>
            {
                opt.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(opt =>
            {
                opt.DocumentTitle = "Curso C#";
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso C# - v1");
                opt.SwaggerEndpoint("/swagger/v2/swagger.json", "Curso C# - v2");
                opt.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private static void DependencyInjectionConfiguration(IServiceCollection services)
        {
            #region Service

            services.AddSingleton<ICarroService, CarroService>();

            #endregion

            #region Repository

            services.AddSingleton<ICarroRepository, CarroRepository>();

            #endregion
        }

        private static string XmlCommentsFilePath
        {
            get
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                return xmlPath;
            }
        }
    }
}
