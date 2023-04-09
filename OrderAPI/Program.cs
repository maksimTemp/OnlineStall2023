using OrderAPI.DataContext;
using OrderAPI.Mapping;
using OrderAPI.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using OrderAPI.Domain;
using MassTransit;

namespace OrderAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<OrdersDataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddTransient<IOrderService, OrdersService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMassTransit(config => {
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
                });
            });

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