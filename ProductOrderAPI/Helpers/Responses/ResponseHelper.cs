namespace dotnet_example_clean_arch_with_entity_framework.Helpers.Responses
{
    public static class ResponseHelper
    {
        public static ApiResponse<T> Success<T>(T data, string message = "Request successful")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> Fail<T>(string message, List<ApiError> errors = null)
        {
            return new ApiResponse<T>(false, message, default, errors);
        }
    }
}
