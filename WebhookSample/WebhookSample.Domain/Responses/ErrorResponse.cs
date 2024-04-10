namespace WebhookSample.Domain.Responses
{
    public class ErrorResponse
    {
        /// <summary>
        /// Status code int
        /// </summary>
        /// <example>200</example>
        public int StatusCode { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        /// ["error1", "error2"]
        public List<object> Errors { get; set; }

        public void AddErrorResponse(int statusCode, List<object> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
