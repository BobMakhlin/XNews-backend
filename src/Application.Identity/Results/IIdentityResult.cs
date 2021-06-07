using System.Collections.Generic;

namespace Application.Identity.Results
{
    /// <summary>
    /// Represents the result of an identity-operation.
    /// It contains the information about identity operation execution.
    /// </summary>
    public interface IIdentityResult
    {
        /// <summary>
        /// Determines whether the identity operation finished successfully.
        /// </summary>
        public bool Succeeded { get; }
        /// <summary>
        /// The collection of errors, occured while operation execution.
        /// Will be an empty collection when <see cref="IIdentityResult.Succeeded"/> equals to <see langword="true"/>.
        /// </summary>
        public IEnumerable<string> Errors { get; }
    }
}