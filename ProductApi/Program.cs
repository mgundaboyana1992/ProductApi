using ProductApi.Models;
using Microsoft.EntityFrameworkCore;
using ProductApi.Repository;

namespace ProductApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:16548/","", "http://localhost:4200")
                                      .AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}