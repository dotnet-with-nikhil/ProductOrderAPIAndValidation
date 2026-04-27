using Serilog;
using System.Diagnostics;

namespace dotnet_example_clean_arch_with_entity_framework.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();            
            var request = context.Request;

            Log.Information("Incoming Request: {Method} {Path} from {IP}",
                request.Method,
                request.Path,
                context.Connection.RemoteIpAddress);

            await _next(context);

            Log.Information("Outgoing Response: {StatusCode} completed in {Elapsed} ms",
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);

        }
    }
}
