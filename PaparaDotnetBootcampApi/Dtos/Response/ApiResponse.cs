using Microsoft.AspNetCore.Http;

namespace PaparaDotnetBootcampApi.Dtos.Result
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccessFul { get; set; }
        public string Message { get; set; }

        public ApiResponse() { }

        public ApiResponse(T data, int statusCode, bool isSuccessFul, string message = null)
        {
            Data = data;
            StatusCode = statusCode;
            IsSuccessFul = isSuccessFul;
            Message = message;
        }

        public static ApiResponse<T> Success(T data, int statusCode = 200, string message = "Request successful.")
        {
            return new ApiResponse<T>(data, statusCode, true, message);
        }

        public static ApiResponse<T> Failure(string message, int statusCode = 400)
        {
            return new ApiResponse<T>(default, statusCode, false, message);
        }
    }
}
