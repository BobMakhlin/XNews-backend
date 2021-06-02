namespace Application.Validation.Options
{
    /// <summary>
    /// Contains <see langword="static readonly"/> fields, used to validate a post.
    /// </summary>
    internal static class PostValidationOptions
    {
        /// <summary>
        /// Determines the minimum count of letters in a title.
        /// </summary>
        public static readonly int TitleMinLength = 16;
        /// <summary>
        /// Determines the maximum count of letters in a title.
        /// </summary>
        public static readonly int TitleMaxLength = 72;
        /// <summary>
        /// Determines the minimum count of letters in content.
        /// </summary>
        public static readonly int ContentMinLength = 64;
        /// <summary>
        /// Determines the maximum count of letters in content.
        /// </summary>
        public static readonly int ContentMaxLength = 100000;
    }
}