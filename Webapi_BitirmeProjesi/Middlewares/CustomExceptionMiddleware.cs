using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Webapi_BitirmeProjesi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private Task HandleException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";

            List<ValidationFailure> errors;
            if (ex.GetType() == typeof(ValidationException))
            {
                httpContext.Response.StatusCode = 400;
                errors = ((ValidationException)ex).Errors.ToList();
                List<string> errDetail = new List<string>();
                foreach (var error in errors)
                {
                    errDetail.Add(error.PropertyName+": "+error.ErrorMessage);
                }
                var errResult = JsonConvert.SerializeObject(new {Errors=errDetail},Formatting.None);
                return httpContext.Response.WriteAsync(errResult);
            }

            if (ex.GetType()==typeof(InvalidOperationException))
            {
                message = ex.Message;
                httpContext.Response.StatusCode = 400;
                var errResult = JsonConvert.SerializeObject(new { Error = message }, Formatting.None);
                return httpContext.Response.WriteAsync(errResult);
            }

            var result = JsonConvert.SerializeObject(new { Error = message }, Formatting.None);
            return httpContext.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
