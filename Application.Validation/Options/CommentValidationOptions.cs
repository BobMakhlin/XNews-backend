namespace Application.Validation.Options
{
    /// <summary>
    /// Contains <see langword="static readonly"/> fields, used to validate a comment.
    /// </summary>
    internal static class CommentValidationOptions
    {
        /// <summary>
        /// Determines the minimum count of letters in content of comment.
        /// </summary>
        public static readonly int ContentMinLength = 2;
        /// <summary>
        /// Determines the maximum count of letters in content of comment.
        /// </summary>
        public static readonly int ContentMaxLength = 320;
    }
}