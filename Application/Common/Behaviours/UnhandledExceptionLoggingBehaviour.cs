using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// Behaviour used to log unhandled exceptions from the MediatR pipeline
    /// (from all behaviours registered after this one), including commands and queries.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class UnhandledExceptionLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        #region Fields

        private readonly ILogger<UnhandledExceptionLoggingBehaviour<TRequest, TResponse>> _logger;

        #endregion

        #region Constructors

        public UnhandledExceptionLoggingBehaviour(ILogger<UnhandledExceptionLoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        #endregion

        #region IPipelineBehavior<TRequest, TResponse>

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                string requestTypeFullName = typeof(TRequest).FullName;
                _logger.LogError(ex, $"Unhandled exception when executing {requestTypeFullName}");
                
                throw;
            }
        }

        #endregion
    }
}