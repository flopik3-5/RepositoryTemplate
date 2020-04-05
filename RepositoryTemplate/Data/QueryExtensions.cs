using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RepositoryTemplate.Specifications;
using RepositoryTemplate.Specifications.Interfaces;

namespace RepositoryTemplate.Data
{
    internal static class QueryExtensions
    {
        internal static IQueryable<TResult> ApplySpecification<TEntity, TResult>(
            this IQueryable<TEntity> baseQuery,
            ISpecification<TEntity, TResult> specification)
            where TEntity : class, IEntity
        {
            var query = baseQuery
                .ApplyDataAccessMode(specification)
                .ApplyIncludes(specification)
                .ApplyFiltering(specification)
                .ApplyOrdering(specification)
                .ApplyPaging(specification);

            return specification switch
            {
                IDataSpecification<TEntity> _ => query.Cast<TResult>(),
                IGroupingSpecification<TEntity, TResult> s => query.ApplyGrouping(s),
                ITransformationSpecification<TEntity, TResult> s => query.ApplyTransformation(s),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        private static IQueryable<TEntity> ApplyDataAccessMode<TEntity, TResult>(
            this IQueryable<TEntity> baseQuery,
            ISpecification<TEntity, TResult> specification)
            where TEntity : class, IEntity
        {
            return specification.IsReadOnly ? baseQuery.AsNoTracking() : baseQuery;
        }

        private static IQueryable<TEntity> ApplyIncludes<TEntity, TResult>(
            this IQueryable<TEntity> baseQuery,
            ISpecification<TEntity, TResult> specification)
            where TEntity : class, IEntity
        {
            var query = specification.IncludeExpressions
                .Aggregate(baseQuery, (current, include) => current.Include(include));

            return specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
        }

        private static IQueryable<TEntity> ApplyFiltering<TEntity, TResult>(
            this IQueryable<TEntity> baseQuery,
            ISpecification<TEntity, TResult> specification)
            where TEntity : class, IEntity
        {
            return specification.FilterExpression == null
                ? baseQuery
                : baseQuery.Where(specification.FilterExpression);
        }

        private static IQueryable<TEntity> ApplyOrdering<TEntity, TResult>(
            this IQueryable<TEntity> baseQuery,
            ISpecification<TEntity, TResult> specification)
            where TEntity : class, IEntity
        {
            return specification.OrderingType switch
            {
                OrderingType.None => baseQuery,
                OrderingType.Ascending => baseQuery.OrderBy(specification.OrderByExpression),
                OrderingType.Descending => baseQuery.OrderByDescending(specification.OrderByExpression),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        private static IQueryable<TEntity> ApplyPaging<TEntity, TResult>(
            this IQueryable<TEntity> baseQuery,
            ISpecification<TEntity, TResult> specification)
            where TEntity : class, IEntity
        {
            return specification.IsPagingEnabled
                ? baseQuery.Skip(specification.Skip).Take(specification.Take)
                : baseQuery;
        }

        private static IQueryable<TResult> ApplyGrouping<TEntity, TResult>(
            this IQueryable<TEntity> baseQuery,
            IGroupingSpecification<TEntity, TResult> specification)
            where TEntity : class, IEntity
        {
            return baseQuery.GroupBy(specification.GroupByExpression)
                .Select(specification.TransformExpression);
        }

        private static IQueryable<TResult> ApplyTransformation<TEntity, TResult>(
            this IQueryable<TEntity> baseQuery,
            ITransformationSpecification<TEntity, TResult> specification)
            where TEntity : class, IEntity
        {
            return baseQuery.Select(specification.TransformExpression);
        }
    }
}