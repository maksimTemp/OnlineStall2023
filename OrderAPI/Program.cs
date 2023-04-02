using OrderAPI.DataContext;
using OrderAPI.Mapping;
using OrderAPI.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using OrderAPI.Domain;
using CatalogAPI.Services;

namespace OrderAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<CatalogDataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddTransient<IProductsService, ProductsService>();
            builder.Services.AddTransient<IOrderService, OrdersService>();
            builder.Services.AddTransient<ICategoryService, CategoriesService>();

            builder.Services.AddControllers();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);


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