using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
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

        private RequestDelegate _next;
        private ILogger<ExceptionHandlerMiddleware> _logger;

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
            catch (NotFoundException ex)
            {
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