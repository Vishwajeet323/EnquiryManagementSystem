using System.Net;

namespace InquiryTrackerApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware>logger,RequestDelegate next) 
        {
            this.logger=logger;
            this.next = next;
        } 

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = new int();

                //Log this Exception
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                //return custome error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";


                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went Wrong! we are looking into resolving this."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
