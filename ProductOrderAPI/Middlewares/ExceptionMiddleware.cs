using dotnet_example_clean_arch_with_entity_framework.Helpers.Responses;
using System.Text.Json;
using Serilog;
using System.Net;

namespace dotnet_example_clean_arch_with_entity_framework.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            // 🔥 Structured logging
            Log.Error(ex,
                "Unhandled Exception occurred. Path: {Path}, Method: {Method}",
                context.Request.Path,
                context.Request.Method);

            var response = ResponseHelper.Fail<string>(
               "Internal server error",
               new List<ApiError>
               {
                    new ApiError
                    {
                        Field = "Exception",
                        ErrorMessage = ex.Message
                    }
               });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);

        }
    }
}
