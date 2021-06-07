using System.Collections.Generic;
using System.Linq;
using Application.Identity.Results;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Results
{
    /// <summary>
    /// Represents a wrapper for <see cref="IdentityResult"/> type. 
    /// </summary>
    public class AspNetIdentityResultWrapper : IIdentityResult
    {
        #region Fields

        private readonly IdentityResult _identityResult;

        #endregion

        #region Constructors

        public AspNetIdentityResultWrapper(IdentityResult identityResult)
        {
            _identityResult = identityResult;
        }

        #endregion

        #region IIdentityResult

        public bool Succeeded => _identityResult.Succeeded;
        public IEnumerable<string> Errors => GetErrors();

        #endregion

        #region Methods

        /// <summary>
        /// Returns the collection of errors of <see cref="_identityResult"/> field.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetErrors()
        {
            return _identityResult.Errors
                .Select(e => e.Description);
        }

        #endregion
    }
}