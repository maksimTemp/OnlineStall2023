using CatalogAPI.DataContext;
using CatalogAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<CatalogDataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddTransient<IProductsService, ProductsService>();
            builder.Services.AddTransient<IProducerService, ProducersService>();
            builder.Services.AddTransient<ICategoryService, CategoriesService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}