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

        #endregion

        #region Constructors

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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
                await HandleValidationExceptionAsync(ex, context);
            }
            catch (NotFoundException ex)
            {
                HandleNotFoundException(ex, context);
            }
            catch (Exception ex)
            {
                HandleUnexpectedException(ex, context);
            }
        }

        /// <summary>
        /// Handles the <paramref name="exception"/> of type <see cref="ValidationException"/> by setting a
        /// response to a client, using the given <paramref name="context"/>.
        /// </summary>
        /// <param name="exception">Exception to be handled</param>
        /// <param name="context">Used to set the response to a client</param>
        /// <returns></returns>
        private async Task HandleValidationExceptionAsync(ValidationException exception, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(exception.Message);
        }

        /// <summary>
        /// Handles the <paramref name="exception"/> of type <see cref="NotFoundException"/> by setting a
        /// response to a client, using the given <paramref name="context"/>.
        /// </summary>
        /// <param name="exception">Exception to be handled</param>
        /// <param name="context">Used to set the response to a client</param>
        /// <returns></returns>
        private void HandleNotFoundException(NotFoundException exception, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }

        /// <summary>
        /// Handles the specified <paramref name="unexpectedException"/> by setting a
        /// response to a client, using the given <paramref name="context"/>.
        /// </summary>
        /// <param name="unexpectedException">Exception to be handled</param>
        /// <param name="context">Used to set the response to a client</param>
        /// <returns></returns>
        private void HandleUnexpectedException(Exception unexpectedException, HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
        
        #endregion
    }
}