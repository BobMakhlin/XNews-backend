using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    /// <summary>
    /// Throws when an identity operation failed.
    /// </summary>
    [Serializable]
    public class IdentityException : Exception
    {
        #region Constructors

        /// <summary>
        /// Creates an instance of type <see cref="IdentityException"/>.
        /// The <see cref="IdentityException.Message"/> will be created based on the specified
        /// <paramref name="errors"/>.
        /// </summary>
        public IdentityException(IEnumerable<string> errors) : base(GetErrorMessageFrom(errors))
        {
            Errors = errors;
        }

        /// <summary>
        /// Creates an instance of type <see cref="IdentityException"/> with the specified
        /// <paramref name="message"/>, saving <paramref name="errors"/>
        /// into <see cref="IdentityException.Errors"/> property.
        /// </summary>
        public IdentityException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }

        /// <summary>
        /// Creates an instance of type <see cref="IdentityException"/> with the specified
        /// <paramref name="message"/>. Property <see cref="IdentityException.Errors"/>
        /// will be equal to an empty collection.
        /// </summary>
        public IdentityException(string message) : base(message)
        {
            Errors = Enumerable.Empty<string>();
        }

        protected IdentityException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Errors, occured during the identity operation execution.
        /// </summary>
        public IEnumerable<string> Errors { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an error message based on the given <paramref name="errorsCollection"/>.
        /// </summary>
        private static string GetErrorMessageFrom(IEnumerable<string> errorsCollection)
        {
            IEnumerable<string> formattedErrors = errorsCollection
                .Select(e => $"{Environment.NewLine} --- {e}");
            string joinedFormattedErrors = string.Join(string.Empty, formattedErrors);
            return $"Identity operation failed: {joinedFormattedErrors}";
        }

        #endregion
    }
}