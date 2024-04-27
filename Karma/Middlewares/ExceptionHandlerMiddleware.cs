using FluentValidation;
using Karma.API.Extensions;
using Karma.Application.Base;
using Karma.Core.Entities.Base;
using Karma.Core.Repositories.Base;
using Newtonsoft.Json;
using System.Net;

namespace Karma.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var logger = context.RequestServices.GetRequiredService<IExceptionLogger>();

            var requestUrl = context.Request.GetFullUrl();

            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationException exception)
            {
                await ConfigureResponse(context, HttpStatusCode.BadRequest, exception.Message);
            }
            catch (UnauthorizedAccessException exception)
            {
                await ConfigureResponse(context, HttpStatusCode.Unauthorized, exception.Message);
            }
            catch (ManagedException exception)
            {
                await ConfigureResponse(context, HttpStatusCode.BadRequest, exception.Message);
            }
            catch (Exception exception)
            {
                var newExceptionLog = ExceptionLog.Create(requestUrl, exception.Message, exception.InnerException?.Message, exception.StackTrace);
                await logger.LogAsync(newExceptionLog);

                Console.WriteLine(exception);
                await ConfigureResponse(context, HttpStatusCode.InternalServerError, "متاسفانه خطای سیستمی رخ داده است، در صورت لزوم با پشتیبانی تماس حاصل نمایید");
            }
        }

        private static async Task ConfigureResponse(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                new FailedResponseMessage(message).ToString());
        }

    }

    public class FailedResponseMessage
    {
        public FailedResponseMessage(string message)
        {
            this.message = message;
        }
        public string message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
