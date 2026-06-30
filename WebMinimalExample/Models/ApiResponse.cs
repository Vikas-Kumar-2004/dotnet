using System.Net;

namespace WebMinimalExample.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = [];
        }

        public bool IsSuccess { get; set; } = true;

        public object? Result { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public List<string?> ErrorMessages { get; set; }
    }
}