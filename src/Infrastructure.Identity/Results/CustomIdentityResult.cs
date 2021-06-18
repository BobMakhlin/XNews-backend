using System.Collections.Generic;
using Application.Identity.Results;

namespace Infrastructure.Identity.Results
{
    /// <summary>
    /// Represents the result of an identity-operation.
    /// It contains the information about identity operation execution.
    /// This type is an implementor of <see cref="IIdentityResult"/>.
    /// </summary>
    public class CustomIdentityResult : IIdentityResult
    {
        #region IIdentityResult

        public bool Succeeded { get; private init; }
        public IEnumerable<string> Errors { get; private init; }

        #endregion

        #region Methods

        /// <summary>
        /// A static-factory method, returning an object of type <see cref="IIdentityResult"/>,
        /// that represents a succeeded result.
        /// </summary>
        public static IIdentityResult Success() =>
            new CustomIdentityResult {Succeeded = true};

        /// <summary>
        /// A static-factory method, returning an object of type <see cref="IIdentityResult"/>,
        /// that represents a failed result. This result will contain the specified <paramref name="errors"/>.
        /// </summary>
        public static IIdentityResult Failed(params string[] errors) =>
            new CustomIdentityResult {Succeeded = false, Errors = errors};

        #endregion
    }
}