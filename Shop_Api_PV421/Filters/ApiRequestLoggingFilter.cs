using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Shop_Api_PV421.Filters
{
    public class ApiRequestLoggingFilter : IAsyncActionFilter
    {
        private readonly ILogger<ApiRequestLoggingFilter> logger;

        public ApiRequestLoggingFilter(ILogger<ApiRequestLoggingFilter> logger)
        {
            this.logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sw = Stopwatch.StartNew();
            var controller = context.ActionDescriptor.RouteValues["controller"];
            var action = context.ActionDescriptor.RouteValues["action"];

            var executedContext = await next();

            sw.Stop();

            logger.LogInformation(
                "{Method} {Path} -> {Controller}.{Action} finished with {StatusCode} in {ElapsedMs} ms",
                context.HttpContext.Request.Method,
                context.HttpContext.Request.Path,
                controller,
                action,
                context.HttpContext.Response.StatusCode,
                sw.ElapsedMilliseconds);
        }
    }
}
