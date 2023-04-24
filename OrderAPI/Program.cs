using MassTransit;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Consumers;
using OrderAPI.DataContext;
using OrderAPI.Mapping;
using OrderAPI.Services;
using SharedLibrary;
using System.Text.Json.Serialization;
using System.Text.Json;

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
            builder.Services.AddTransient<IOrderItemService, OrderItemsService>();

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

            //builder.Services.AddMassTransit(config =>
            //{
            //    config.AddConsumer<ItemChangedConsumer>();
            //    config.AddConsumer<ProductDeletedMessageConsumer>();
            //    config.AddConsumer<DeliveryStatusChangedConsumer>();

            //    config.UsingRabbitMq((context, configuration) =>
            //    {
            //        configuration.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

            //        configuration.ReceiveEndpoint(QueuesUrls.CatalogProductNameChanged, c =>
            //        {
            //            c.ConfigureConsumer<ItemChangedConsumer>(context);
            //        });
            //        configuration.ReceiveEndpoint(QueuesUrls.CatalogProductDeleted, c =>
            //        {
            //            c.ConfigureConsumer<ProductDeletedMessageConsumer>(context);
            //        });
            //        configuration.ReceiveEndpoint(QueuesUrls.DeliveryStatusChanged, c =>
            //        {
            //            c.ConfigureConsumer<DeliveryStatusChangedConsumer>(context);
            //        });
            //    });
            //});
            var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
            builder.Services.AddMassTransit(mt => mt.UsingRabbitMq((cntxt, cfg) => {
                cfg.Host(rabbitMqSettings.Uri, "/", c => {
                    c.Username(rabbitMqSettings.UserName);
                    c.Password(rabbitMqSettings.Password);
                });
                cfg.ReceiveEndpoint("samplequeue", (c) => {
                    c.Consumer<CommandMessageConsumer>();
                });
            }));

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