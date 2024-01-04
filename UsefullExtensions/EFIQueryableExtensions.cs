using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace UsefullExtensions
{
    /// <summary>
    /// These methods can be useful when working with complex object graphs
    /// and you want to eagerly load related entities.
    /// </summary>
    public static class EFIQueryableExtensions
    {
        public static IQueryable<TEntity> IncludeManyWithThenInclude<TEntity, TProperty>(
            [NotNull] this IQueryable<TEntity> source,
            [NotNull] Expression<Func<TEntity, TProperty>> navigationPropertyPath,
            [NotNull] params Expression<Func<TProperty, object>>[] nextProperties)
            where TEntity : class
        {
            foreach (var nextProperty in nextProperties)
            {
                source = source.Include(navigationPropertyPath)
                    .ThenInclude(nextProperty);
            }

            return source;
        }

        public static IQueryable<TEntity> IncludeManyWithThenInclude<TEntity, TProperty>(
            [NotNull] this IQueryable<TEntity> source,
            [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> navigationPropertyPath,
            [NotNull] params Expression<Func<TProperty, object>>[] nextProperties)
            where TEntity : class
        {
            foreach (var nextProperty in nextProperties)
            {
                source = source.Include(navigationPropertyPath)
                    .ThenInclude(nextProperty);
            }

            return source;
        }

        public static IQueryable<TEntity> IncludeMany<TEntity>(
            [NotNull] this IQueryable<TEntity> source,
            [NotNull] params Expression<Func<TEntity, object>>[] navigationProperties)
            where TEntity : class
        {
            foreach (var property in navigationProperties)
            {
                source = source.Include(property);
            }

            return source;
        }
    }
}