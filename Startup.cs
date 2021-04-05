using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace apiDio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Data.MongoDB>();
            services.AddControllers();

            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title="API DE PACIENTES",Version ="v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Utiliza a biblioteca do Swagegr para facilitar a documentação da API
            app.UseSwagger(); //Gera um arquivo JSON
            app.UseSwaggerUI(config => { //Views HTML do Swagger
                config.SwaggerEndpoint("/swagger/v1/swagger.json","v1 docs");
            });
        }
    }
}

//link do swagger: https://localhost:5001/swagger/index.html