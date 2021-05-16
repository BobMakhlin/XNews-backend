namespace Application.Validation.Options
{
    /// <summary>
    /// Contains <see langword="static readonly"/> fields, used to validate a category.
    /// </summary>
    internal static class CategoryValidationOptions
    {
        /// <summary>
        /// Determines the minimum count of letters in a title.
        /// </summary>
        public static readonly int TitleMinLength = 2;
        /// <summary>
        /// Determines the maximum count of letters in a title.
        /// </summary>
        public static readonly int TitleMaxLength = 32;
    }
}