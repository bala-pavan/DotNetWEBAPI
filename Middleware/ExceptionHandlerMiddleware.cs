using System.Net;

namespace WebSampleApplicationAPI.Middleware
{
    
    /*public class ExceptionHandlerMiddleware( ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
    {
        this.logger= logger;
        this.next=next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(httpContext);
        }
        catch(Exception ex)
        {
            var errorId = Guid.NewGuid();
            //Log This Exception
            logger.LogError(ex, ex.Message);

            //return a custom error response
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContextType = "application/json";

            var error = new
            {
                Id = errorId,
                ErrorMessage = "Omething went wrong"
            };
            await httpContext.Response.WriteAsJsonAsync().JsonAsync();
        }

    }*/
}
