using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using WebhookSample.API.Consumer;
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
            services.AddTransient<IEventService, EventService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClientContext>(options =>
                options.UseSqlServer(connectionString: configuration.GetConnectionString("Client"))
            );

            services.AddScoped<IClientRepository, ClientRepository>();
        }

        public static void ConfigureRabbit(this IServiceCollection services, IConfiguration configuration)
        {
            var configRabbit = configuration.GetSection("RabbitMQ");
            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq((context, rabbitMqConfig) =>
                {
                    rabbitMqConfig.Host(configRabbit.GetValue<string>("Host"), hostConfig =>
                    {
                        hostConfig.Username(configRabbit.GetValue<string>("Username"));
                        hostConfig.Password(configRabbit.GetValue<string>("Password"));
                    });
                    rabbitMqConfig.ReceiveEndpoint("clients", c =>
                    {
                        c.Consumer<EventConsumer>();
                    });
                });

            });
        }
    }
}
