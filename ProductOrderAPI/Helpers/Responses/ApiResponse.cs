namespace dotnet_example_clean_arch_with_entity_framework.Helpers.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<ApiError> Errors { get; set; }

        public ApiResponse(bool success, string message, T data = default, List<ApiError> errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = Errors;
        }
    }
}
