
using Microsoft.OpenApi.Models;
using pi4_PixieVault_DemiBruls.Database;
using System.Reflection;

namespace pi4_API_DemiBruls
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DatabaseContext>();
            builder.Services.AddSwaggerGen(
                c =>
                {
                    c.EnableAnnotations();
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DemoSwaggerAnnotation", Version = "v1" });
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
