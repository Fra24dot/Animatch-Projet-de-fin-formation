namespace Animatch.Api.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // On laisse la requête continuer normalement
                await next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, ex.Message, StatusCodes.Status401Unauthorized);
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(context, ex.Message, StatusCodes.Status409Conflict);
            }
            catch (Exception ex)
            {
                // Toute autre exception non gérée → 500
                await HandleExceptionAsync(context, "An unexpected error occurred", StatusCodes.Status500InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(new
            {
                message,
                status = statusCode
            });
        }
    }
}
