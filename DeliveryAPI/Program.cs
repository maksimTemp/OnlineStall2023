using DeliveryAPI.DataContext;
using DeliveryAPI.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MassTransit;
using SharedLibrary;
using DeliveryAPI.Consumers;
using DeliveryAPI.Mapping;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DeliveryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<DeliveryDataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddTransient<IDeliveryService, DeliveryService>();
            builder.Services.AddTransient<IDeliveryItemService, DeliveryItemsService>();
            
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddControllers(options =>
            {
                options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
                options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                }));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<ItemChangedConsumer>();
                config.AddConsumer<ProductDeletedMessageConsumer>();
                config.AddConsumer<DeliveryCreateMessageConsumer>();

                config.UsingRabbitMq((context, configuration) =>
                {
                    configuration.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

                    configuration.ReceiveEndpoint(QueuesUrls.CatalogProductNameChanged, c =>
                    {
                        c.ConfigureConsumer<ItemChangedConsumer>(context);
                    });
                    configuration.ReceiveEndpoint(QueuesUrls.CatalogProductDeleted, c =>
                    {
                        c.ConfigureConsumer<ProductDeletedMessageConsumer>(context);
                    });
                    configuration.ReceiveEndpoint(QueuesUrls.DeliveryCreated, c =>
                    {
                        c.ConfigureConsumer<DeliveryCreateMessageConsumer>(context);
                    });
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