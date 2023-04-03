using UserAPI.DataContext;
using UserAPI.Mapping;
using UserAPI.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using UserAPI.Domain;

namespace UserAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<UsersDataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddTransient<IIdentityService, IdentitiesService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
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