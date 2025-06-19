using Fase4Cap7WebserviceASPNET.Main.Data;
using Microsoft.EntityFrameworkCore;

namespace Fase4Cap7WebserviceASPNET.Main
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Fase4Cap7WebserviceASPNET.xml");
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
