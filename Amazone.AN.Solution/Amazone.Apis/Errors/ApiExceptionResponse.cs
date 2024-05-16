namespace Amazone.Apis.Errors
{
    public class ApiExceptionResponse : ResponseApi
    {
        public ApiExceptionResponse(int statucode, string? message = null, string? details = null) : base(statucode, message)
        {
            Details = details;
        }

        public string? Details { get; set; }
    }
}
