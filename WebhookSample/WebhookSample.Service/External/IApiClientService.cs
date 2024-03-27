using Refit;
using WebhookSample.Domain.Requests;
using WebhookSample.Domain.Responses;

namespace WebhookSample.Service.External
{
    public interface IApiClientService
    {
        [Post("")]
        Task<ApiResponse<GenericApiResponse>> SendClientNotification(EventNotification notification);
    }
}
