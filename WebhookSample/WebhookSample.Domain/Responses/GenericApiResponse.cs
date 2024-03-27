namespace WebhookSample.Domain.Responses
{
    public class GenericApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsOk { get; set; }
        public object Data { get; set; }
    }
}
