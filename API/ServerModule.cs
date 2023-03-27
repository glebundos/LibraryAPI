using Application.Commands;
using Application.Handlers.CommandHandlers;
using Application.Helpers;
using Core.Repositories;
using Core.Repositories.Base;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace API
{
    public static class ServerModule
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();
            services.AddControllers().AddJsonOptions(
            o =>
            {
                o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            services.AddEndpointsApiExplorer();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<LibraryContext>(m => m.UseSqlServer(connectionString));
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Library.API",
                    Version = "v1"
                });
            });

            services.AddCors();
            services.AddAutoMapper(typeof(Program));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(CreateBookCommand))));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITokenService, TokenService>();

            AppSettings appSettings = new AppSettings();
            builder.Configuration.GetSection("AppSettings").Bind(appSettings);
            services.AddSingleton<AppSettings>(appSettings);
        }
    }
}
