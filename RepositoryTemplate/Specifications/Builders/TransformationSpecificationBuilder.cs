using System;
using System.Linq.Expressions;
using RepositoryTemplate.Data;

namespace RepositoryTemplate.Specifications.Builders
{
    internal class TransformationSpecificationBuilder<TEntity, TResult>
        : SpecificationBuilder<
            TransformationSpecificationBuilder<TEntity, TResult>,
            TransformationSpecification<TEntity, TResult>,
            TEntity,
            TResult>
        where TEntity : IEntity
    {
        public TransformationSpecificationBuilder<TEntity, TResult> WithTransform(
            Expression<Func<TEntity, TResult>> transformExpression)
        {
            Specification.TransformExpression = transformExpression;

            return Builder;
        }

        protected override TransformationSpecificationBuilder<TEntity, TResult> Builder => this;
    }
}