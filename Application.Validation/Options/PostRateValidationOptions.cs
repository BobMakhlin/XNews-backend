namespace Application.Validation.Options
{
    /// <summary>
    /// Contains <see langword="static readonly"/> fields, used to validate a post rate.
    /// </summary>
    public static class PostRateValidationOptions
    {
        /// <summary>
        /// Determines the value of a negative rate (i.e., dislike).
        /// </summary>
        public static readonly int NegativeRate = -1;

        /// <summary>
        /// Determines the value of a positive rate (i.e., like).
        /// </summary>
        public static readonly int PositiveRate = +1;
    }
}