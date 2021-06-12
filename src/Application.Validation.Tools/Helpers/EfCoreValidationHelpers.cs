using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.Tools.Helpers
{
    internal static class EfCoreValidationHelpers
    {
        /// <summary>
        /// Determines whether the specified <paramref name="newValue"/> is unique within the
        /// column of the given <paramref name="dbSet"/>.
        /// Column is specified by the parameter <paramref name="getColumnSelector"/>.
        /// </summary>
        /// <param name="dbSet"></param>
        /// <param name="getColumnSelector">
        /// Determines the column, with which we will compare <paramref name="newValue"/>
        /// </param>
        /// <param name="newValue">
        /// Value, that will be checked for uniqueness
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TColumn"></typeparam>
        /// <returns></returns>
        public static async Task<bool> IsValueUniqueInsideOfDbSetColumnAsync<TEntity, TColumn>(DbSet<TEntity> dbSet,
            Expression<Func<TEntity, TColumn>> getColumnSelector,
            TColumn newValue,
            CancellationToken cancellationToken)
            where TEntity : class
        {
            return !await dbSet
                .Select(getColumnSelector)
                .AnyAsync(column => column.Equals(newValue), cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// <para>
        ///     Checks if the given <paramref name="values"/> are unique for the columns combination,
        ///     specified by <paramref name="getColumnsCombination"/>, in <paramref name="dbSet"/>.
        /// </para>
        /// <para>
        ///     By columns combination we mean an object, containing multiple columns,
        ///     <para>e.g. <c>comment => new { comment.CommentId, comment.UserId }</c>.</para>
        ///     The method will build the following expression for this columns combination:
        ///     <para><c>comment.CommentId == values[0] AND comment.UserId == values[1]</c>.</para>
        ///     Then this expression will be passed into <see cref="o:EntityFrameworkQueryableExtensions.AnyAsync"/>
        ///     and the result of this method will be returned.
        /// </para>
        /// </summary>
        /// <param name="dbSet"></param>
        /// <param name="getColumnsCombination">
        /// Determines the columns combination with which we will compare the specified <paramref name="values"/>
        /// one by one (the first column with the first item of <paramref name="values"/> and so on).
        /// </param>
        /// <param name="values">
        /// Values that will be checked for uniqueness for the specified columns combination.
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TColumnsCombination"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// When the type of <paramref name="getColumnsCombination.Body"/> is not <see cref="NewExpression"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When the count of columns, specified by <paramref name="getColumnsCombination"/> is not equal to
        /// the count of the given <paramref name="values"/>.
        /// </exception>
        public static async Task<bool> AreValuesUniqueForColumnsCombinationInDbSetAsync<TEntity, TColumnsCombination>
        (
            DbSet<TEntity> dbSet,
            Expression<Func<TEntity, TColumnsCombination>> getColumnsCombination,
            object[] values,
            CancellationToken cancellationToken
        )
            where TEntity : class
        {
            if (!(getColumnsCombination.Body is NewExpression))
            {
                throw new ArgumentException(
                    $"The property {getColumnsCombination.Body} must be of type {nameof(NewExpression)}",
                    nameof(getColumnsCombination));
            }

            NewExpression combinationOfColumnsExpression = (NewExpression) getColumnsCombination.Body;
            ReadOnlyCollection<Expression> columnExpressions = combinationOfColumnsExpression.Arguments;

            if (columnExpressions.Count != values.Length)
            {
                throw new ArgumentException("The count of values must be equal to the count of columns",
                    nameof(values));
            }

            Expression predicateBody = CompareForEqualityExpressionsWithValues(columnExpressions, values);
            ParameterExpression entity = getColumnsCombination.Parameters[0];
            Expression<Func<TEntity, bool>> predicate = Expression.Lambda<Func<TEntity, bool>>(predicateBody, entity);

            return !await dbSet
                .AnyAsync(predicate, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Builds an expression, where it compares for equality each item of <paramref name="expressions"/> with appropriate
        /// item of <paramref name="values"/>. It compares them one by one.
        /// </summary>
        private static Expression CompareForEqualityExpressionsWithValues(ReadOnlyCollection<Expression> expressions,
            object[] values)
        {
            Expression predicateBody = Expression.Equal(expressions[0], Expression.Constant(values[0]));

            for (int i = 1; i < expressions.Count; i++)
            {
                Expression compareValueWithCurrentExpression =
                    Expression.Equal(expressions[i], Expression.Constant(values[i]));
                predicateBody = Expression.AndAlso(predicateBody, compareValueWithCurrentExpression);
            }

            return predicateBody;
        }
    }
}