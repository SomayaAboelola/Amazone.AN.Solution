namespace Amazone.Apis.Errors
{
    public class ValidationErrorResponse : ResponseApi
    {
        public ValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
