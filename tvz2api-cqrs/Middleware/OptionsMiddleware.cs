using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace tvz2api_cqrs.Middleware
{
  public class OptionsMiddleware
  {
    private readonly RequestDelegate _next;

    public OptionsMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public Task Invoke(HttpContext context)
    {
      return BeginInvoke(context);
    }

    private Task BeginInvoke(HttpContext context)
    {
      context.Response.Headers.Add("Access-Control-Allow-Origin", new [] { "http://localhost:8080/" });

      if (context.Request.Method == "OPTIONS")
      {
        context.Response.Headers.Add("Access-Control-Allow-Headers", new [] { "Origin, X-Requested-With, Content-Type, Accept" });
        context.Response.Headers.Add("Access-Control-Allow-Methods", new [] { "GET, POST, PUT, DELETE, OPTIONS" });
        context.Response.Headers.Add("Access-Control-Allow-Credentials", new [] { "true" });
        context.Response.StatusCode = 200;
        return context.Response.WriteAsync("OK");
      }

      return _next.Invoke(context);
    }
  }

  public static class OptionsMiddlewareExtensions
  {
    public static IApplicationBuilder UseOptions(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<OptionsMiddleware>();
    }
  }
}