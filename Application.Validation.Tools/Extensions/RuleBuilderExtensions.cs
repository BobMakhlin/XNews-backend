using System;
using System.Linq;
using FluentValidation;

namespace Application.Validation.Tools.Extensions
{
    /// <summary>
    /// Extension methods for type <see cref="IRuleBuilder{T,TProperty}"/>
    /// </summary>
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// Defines an 'in' validator on the current rule builder.
        /// Validation will fail if the value of the property is not equal
        /// to any value of the specified <paramref name="validValues"/>.
        /// </summary>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <param name="validValues"></param>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <typeparam name="TProperty">Type of property being validated</typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException">
        /// If parameter <paramref name="validValues"/> doesn't contain any items.
        /// </exception>
        public static IRuleBuilderOptions<T, TProperty> In<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder,
            params TProperty[] validValues)
        {
            if (validValues == null)
            {
                throw new ArgumentNullException(nameof(validValues));
            }
            if (validValues.Length == 0)
            {
                throw new ArgumentException("Provide at least one valid value", nameof(validValues));
            }

            string validValuesString = string.Join(", ", validValues);
            return ruleBuilder
                .Must(validValues.Contains)
                .WithMessage($"{{PropertyName}} must be one of these values: {validValuesString}");
        }
    }
}