using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Refit;
using System.Text.Json;
using WebhookSample.API.Consumer;
using WebhookSample.Data.Context;
using WebhookSample.Data.Repositories;
using WebhookSample.Domain.Interfaces.Repositories;
using WebhookSample.Domain.Interfaces.Services;
using WebhookSample.Service.External;
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

        private static void AddRefit(IServiceCollection services, IConfiguration configuration)
        {
            string baseUrl = configuration.GetValue<string>("BaseWebhookUrl");
            services.AddRefitClient<IApiClientService>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(baseUrl);
            });
        }

        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            AddRefit(services, configuration);

            services.AddDbContext<ClientContext>(options =>
                options.UseSqlServer(connectionString: configuration.GetConnectionString("Client"))
            );

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        public static void ConfigureRabbit(this IServiceCollection services, IConfiguration configuration)
        {
            var configRabbit = configuration.GetSection("RabbitMQ");
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<EventConsumer>();

                cfg.UsingRabbitMq((context, rabbitMqConfig) =>
                {
                    rabbitMqConfig.Host(configRabbit.GetValue<string>("Host"), hostConfig =>
                    {
                        hostConfig.Username(configRabbit.GetValue<string>("Username"));
                        hostConfig.Password(configRabbit.GetValue<string>("Password"));
                    });
                    rabbitMqConfig.ReceiveEndpoint("clients", c =>
                    {
                        c.ConfigureConsumer<EventConsumer>(context);
                    });
                });

            });
        }
    }
}
