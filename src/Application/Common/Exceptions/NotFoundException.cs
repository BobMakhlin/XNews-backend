using System;
using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        #region Constructors

        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Creates an instance of type <see cref="NotFoundException"/>.
        /// This constructor can be used when you tried to find an element
        /// with the specified <paramref name="primaryKeyValues"/> in the entity with the name
        /// <paramref name="entityName"/>, but you didn't find anything.
        /// </summary>
        public NotFoundException(string entityName, params object[] primaryKeyValues)
            : base($"Entity \"{entityName}\" ({JoinPrimaryKeyValues(primaryKeyValues)}) was not found.")
        {
        }

        protected NotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Joins the specified <paramref name="primaryKeyValues"/> into a string.
        /// </summary>
        private static string JoinPrimaryKeyValues(object[] primaryKeyValues)
            => string.Join(", ", primaryKeyValues);

        #endregion
    }
}