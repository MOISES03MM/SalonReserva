using BookingEventos.Domain.Exception;
using System.Net;

namespace BookingEventos.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Permite que la petición siga su curso normal
                await _next(context);
            }
            catch (NegocioException ex)
            {
                // Si ocurre una NegocioException, la atrapamos aquí
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (System.Exception ex)
            {
                // Si es cualquier otro error inesperado (ej. se cayó la BD)
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, System.Exception exception, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                mensaje = exception.Message // El mensaje que pusiste en el 'throw'
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
