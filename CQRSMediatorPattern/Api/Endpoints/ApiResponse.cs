namespace CQRSMediatorPattern.Api.Endpoints
{

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        public static ApiResponse<T> Ok(T data, string? message = null) => new() { Success = true, Data = data, Message = message };
        public static ApiResponse<T> Fail(string message, T? data = default) => new() { Success = false, Data = data, Message = message };
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public static ApiResponse Ok(string? message = null) => new() { Success = true, Message = message };
        public static ApiResponse Fail(string message) => new() { Success = false, Message = message };
    }

}
