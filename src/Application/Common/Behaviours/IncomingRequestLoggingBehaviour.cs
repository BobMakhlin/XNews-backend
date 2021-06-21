using System.Threading;
using System.Threading.Tasks;
using Application.Identity.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// Logs all incoming requests.
    /// </summary>
    public class IncomingRequestLoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        #region Fields

        private readonly ILogger<IncomingRequestLoggingBehaviour<TRequest>> _logger;
        private readonly ICurrentLoggedInUserService _currentLoggedInUserService;

        #endregion

        #region Constructors

        public IncomingRequestLoggingBehaviour(ILogger<IncomingRequestLoggingBehaviour<TRequest>> logger,
            ICurrentLoggedInUserService currentLoggedInUserService)
        {
            _logger = logger;
            _currentLoggedInUserService = currentLoggedInUserService;
        }

        #endregion

        #region IRequestPreProcessor<TRequest>

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            string userId = _currentLoggedInUserService.GetUserId();
            string requestTypeFullName = typeof(TRequest).FullName;

            _logger.LogTrace(userId != null
                ? $"Authenticated user ({userId}) sent the request {requestTypeFullName}"
                : $"Non authenticated user sent the request {requestTypeFullName}");

            return Task.CompletedTask;
        }

        #endregion
    }
}