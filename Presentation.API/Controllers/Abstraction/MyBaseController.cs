using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.API.Controllers
{
    /// <summary>
    /// Represents a base type for all controllers.
    /// </summary>
    public class MyBaseController : ControllerBase
    {
        #region Fields

        private IMediator _mediator;

        #endregion

        #region Properties

        /// <summary>
        /// Retrieves the registered service of type <see cref="IMediator"/>.
        /// </summary>
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        #endregion
    }
}