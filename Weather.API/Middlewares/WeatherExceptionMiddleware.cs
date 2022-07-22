using Microsoft.AspNetCore.Http;
using System;
using System.Net;

using System.Threading.Tasks;
using Weather.API.Exceptions;
using System.Net.Mime;
using System.Text.Json;
using System.Net.Http;

namespace Weather.API.Middlewares
{
    public class WeatherExceptionMiddleware : IMiddleware
    {
        private readonly IRequestExceptionHandler requestExceptionHandler;     

        public WeatherExceptionMiddleware(IRequestExceptionHandler request)
        {
            this.requestExceptionHandler = request;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            /*
            catch (WeatherBadRequestException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(e.Message);
            }
            catch (WeatherNotFoundException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(e.Message);
            }
            */
            catch (HttpRequestException e)
            {
                var returnError = requestExceptionHandler.HandleException(e);
                if (!context.Response.HasStarted)
                {
                    context.Response.Clear();
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    context.Response.StatusCode = (int)e.StatusCode; 
                    await context.Response.WriteAsync(JsonSerializer.Serialize(returnError));
                }
            }
            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(e.Message);
            }
        }
         
    }
}
