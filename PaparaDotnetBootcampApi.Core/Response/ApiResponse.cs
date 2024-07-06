
namespace PaparaDotnetBootcampApi.Core.Response
{


    public partial class ApiResponse
    {
        public bool IsSuccessFul { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public ApiResponse(string message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                IsSuccessFul = true;
            }
            else
            {
                IsSuccessFul = false;
                Message = message;
            }
        }
        public ApiResponse(int statusCode, bool isSuccessFul, string message)
        {
            IsSuccessFul = isSuccessFul;
            Message = message;
            StatusCode = statusCode;
        }


        public static ApiResponse Failure(string message, int statusCode = 400)
        {
            return new ApiResponse(statusCode, false, message);
        }

    }


    public partial class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccessFul { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(T data, int statusCode, bool isSuccessFul, string message = null)
        {
            Data = data;
            StatusCode = statusCode;
            IsSuccessFul = isSuccessFul;
            Message = message;
        }
        public ApiResponse(int statusCode, bool isSuccessFul, string message = null)
        {
            StatusCode = statusCode;
            IsSuccessFul = isSuccessFul;
            Message = message;
        }

        public static ApiResponse<T> Success(T data, int statusCode = 200, string message = "Request successful.")
        {
            return new ApiResponse<T>(data, statusCode, true, message);
        }

        public static ApiResponse<T> Success(int statusCode = 200, string message = "Request successful.")
        {
            return new ApiResponse<T>(statusCode, true, message);
        }

        public static ApiResponse<T> Failure(string message, int statusCode = 400)
        {
            return new ApiResponse<T>(default, statusCode, false, message);
        }
    }
}
