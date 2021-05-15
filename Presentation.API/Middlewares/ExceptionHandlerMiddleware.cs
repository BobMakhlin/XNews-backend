using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Presentation.API.Middlewares
{
    /// <summary>
    /// Middleware used to handle exceptions of the all next middlewares.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        #endregion

        #region Constructors

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        #endregion

        #region Methods

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "The middleware caught an exception");
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "The middleware caught an exception");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
        
        #endregion
    }
}