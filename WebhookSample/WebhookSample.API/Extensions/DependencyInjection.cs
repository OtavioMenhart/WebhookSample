using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebhookSample.Data.Context;
using WebhookSample.Data.Repositories.Clients;
using WebhookSample.Domain.Interfaces.Repositories.Clients;
using WebhookSample.Domain.Interfaces.Services;
using WebhookSample.Service.Services;
using WebhookSample.Service.Validators;

namespace WebhookSample.API.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ClientValidator>(ServiceLifetime.Transient);
            services.AddAutoMapper(typeof(Mappers));

            services.AddTransient<IClientService, ClientService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClientContext>(options =>
                options.UseSqlServer(connectionString: configuration.GetConnectionString("Client"))
            );

            services.AddScoped<IClientRepository, ClientRepository>();
        }
    }
}
