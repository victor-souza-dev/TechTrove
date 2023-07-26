using System.Text;

namespace Back;

public class MiddlewareAuthentication
{
    private readonly RequestDelegate _next;
    private readonly string _relm;

    public MiddlewareAuthentication(RequestDelegate next, string relm)
    {
        _next = next;
        _relm = relm;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if(!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized", Encoding.UTF8);
            return;
        }

        var header = context.Request.Headers["Authorization"];
        var encodeCreds = header.ToString().Substring(6);

        if(encodeCreds.Length == 0)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);

    }
}
