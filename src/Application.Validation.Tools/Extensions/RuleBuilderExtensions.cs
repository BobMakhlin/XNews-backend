using System;
using System.Linq;
using System.Linq.Expressions;
using Application.Validation.Tools.Helpers;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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


        /// <summary>
        /// Defines a 'unique inside of DbSet' validator on the current rule builder.
        /// Validation will fail if the value of the property is not unique within the column
        /// of the given <paramref name="dbSet"/>.
        /// The column is specified by <paramref name="getColumnSelector"/>.
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <param name="dbSet"></param>
        /// <param name="getColumnSelector">
        /// Determines the column, with which we will compare the value of the property.
        /// </param>
        /// <typeparam name="TObject">
        /// Type of object being validated
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// Type of property being validated
        /// </typeparam>
        /// <typeparam name="TEntity">
        /// Type of entity, with which <paramref name="dbSet"/> operates
        /// </typeparam>
        /// <returns></returns>
        public static IRuleBuilderOptions<TObject, TProperty> UniqueInsideOfDbSetColumn<TObject, TProperty, TEntity>(
            this IRuleBuilder<TObject, TProperty> ruleBuilder,
            DbSet<TEntity> dbSet, Expression<Func<TEntity, TProperty>> getColumnSelector)
            where TObject : class
            where TEntity : class
        {
            return ruleBuilder
                .MustAsync
                (
                    (newTitle, token) => EfCoreValidationHelpers.IsValueUniqueInsideOfDbSetColumnAsync
                        (dbSet, getColumnSelector, newTitle, token)
                )
                .WithMessage("{PropertyName} must be unique");
        }

        /// <summary>
        /// Checks if the given values are unique for the columns combination,
        /// specified by <paramref name="getColumnsCombination"/>, in <paramref name="dbSet"/>.
        /// </summary>
        /// <param name="ruleBuilder"></param>
        /// <param name="dbSet"></param>
        /// <param name="getColumnsCombination">
        /// Determines the columns combination.
        /// By columns combination we mean object, containing multiple columns,
        /// <para>e.g. <c>comment => new { comment.CommentId, comment.UserId }</c>.</para>
        /// </param>
        public static IRuleBuilderOptions<TObject, object[]> UniqueForColumnsCombinationInDbSet
            <TObject, TColumnsCombination, TEntity>
            (
                this IRuleBuilder<TObject, object[]> ruleBuilder,
                DbSet<TEntity> dbSet,
                Expression<Func<TEntity, TColumnsCombination>> getColumnsCombination
            )
            where TObject : class
            where TEntity : class
        {
            return ruleBuilder
                .MustAsync
                (
                    (values, token) => EfCoreValidationHelpers.AreValuesUniqueForColumnsCombinationInDbSetAsync
                        (dbSet, getColumnsCombination, values, token)
                )
                .WithMessage("The given values are not unique for the specified columns combination");
        }
    }
}