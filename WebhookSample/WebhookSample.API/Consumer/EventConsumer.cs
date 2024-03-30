using MassTransit;
using Newtonsoft.Json;
using WebhookSample.Domain.Entities;
using WebhookSample.Domain.Interfaces.Repositories;
using WebhookSample.Domain.Requests;
using WebhookSample.Service.External;

namespace WebhookSample.API.Consumer
{
    public class EventConsumer : IConsumer<EventNotification>
    {
        private readonly IApiClientService _apiClientService;
        private readonly IBaseRepository<WebhookEventStatus> _webhookEventStatusRepository;

        public EventConsumer(IApiClientService apiClientService, IBaseRepository<WebhookEventStatus> webhookEventStatusRepository)
        {
            _apiClientService = apiClientService;
            _webhookEventStatusRepository = webhookEventStatusRepository;
        }

        public Task Consume(ConsumeContext<EventNotification> context)
        {
            var message = context.Message;
            var result = _apiClientService.SendClientNotification(message);
            result.Wait();

            var webhookStatus = new WebhookEventStatus(
                message.EventName,
                message.EventName.Split("_")[0],
                JsonConvert.SerializeObject(message.Info.ToString().Replace("\r\n", "").Replace("      ", "")),
                result.Result.IsSuccessStatusCode);
            var statusResult = _webhookEventStatusRepository.InsertAsync(webhookStatus);
            statusResult.Wait();

            return Task.CompletedTask;
        }
    }
}
