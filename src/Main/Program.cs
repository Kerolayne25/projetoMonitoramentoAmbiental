using Fase4Cap7WebserviceASPNET.Main.Data;
using Microsoft.EntityFrameworkCore;

namespace Fase4Cap7WebserviceASPNET
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
