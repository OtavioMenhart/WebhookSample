namespace WebhookSample.Domain.Responses
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public List<object> Errors { get; set; }

        public void AddErrorResponse(int statusCode, List<object> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
