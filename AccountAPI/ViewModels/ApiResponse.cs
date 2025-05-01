using Domain.Entities;

namespace AccountAPI.ViewModels
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public static ApiResponse<T> CreateResponse<T>(bool success, string message, T data = default)
        {
            return new ApiResponse<T>
            {
                Success = success,
                Message = message,
                Data = data
            };
        }
    }
}
