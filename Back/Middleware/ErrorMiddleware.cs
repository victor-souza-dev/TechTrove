using Back.Models.View;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace Back.Middleware;
public class ErrorMiddleware
{
    private readonly RequestDelegate next;

    public ErrorMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        ErrorResponse errorResponseVm;

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Qa")
    {
        errorResponseVm = new ErrorResponse(HttpStatusCode.InternalServerError.ToString(),
        $"{ex.Message} {ex?.InnerException?.Message}");
    }
    else
    {
        errorResponseVm = new ErrorResponse(HttpStatusCode.InternalServerError.ToString(),
        "An internal server error has occurred.");
    }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(errorResponseVm);
        context.Response.ContentType = "application/json";
        Log.Error(result.ToString(), DateTime.Now);
        return context.Response.WriteAsync(result);
    }
}