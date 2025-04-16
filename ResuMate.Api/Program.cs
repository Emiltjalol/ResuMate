
using Microsoft.OpenApi.Models;

namespace ResuMate.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddHttpClient();

            var apiKey = builder.Configuration["MY_API_KEY"];           

            // Lägg till Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Resumate API",
                    Version = "v1",
                    Description = "Ett API för Resumate-projektet",
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();
            app.UseCors("AllowAll");


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resumate API v1");
                    c.RoutePrefix = string.Empty; // Gör så att Swagger är startsidan
                });
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();


            

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
