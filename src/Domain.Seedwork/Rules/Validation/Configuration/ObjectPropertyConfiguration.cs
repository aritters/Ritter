using Ritter.Domain.Seedwork.Specs;
using System;
using System.Linq.Expressions;

namespace Ritter.Domain.Seedwork.Rules.Validation.Configuration
{
    public class ObjectPropertyConfiguration<TEntity, TProp> : BasePropertyConfiguration<TEntity, TProp>
        where TEntity : class
        where TProp : class
    {
        public ObjectPropertyConfiguration(ValidationFeature<TEntity> feature, Expression<Func<TEntity, TProp>> expression)
            : base(feature, expression)
        {
        }

        public virtual ObjectPropertyConfiguration<TEntity, TProp> IsRequired()
        {
            return IsRequired(null);
        }

        public virtual ObjectPropertyConfiguration<TEntity, TProp> IsRequired(string message)
        {
            Feature.AddRule(new RequiredRule<TEntity, TProp>(Expression, message));
            return this;
        }

        public ObjectPropertyConfiguration<TEntity, TProp> HasCustom(Func<TEntity, bool> validateFunc)
        {
            return HasCustom(validateFunc, null);
        }

        public ObjectPropertyConfiguration<TEntity, TProp> HasCustom(Func<TEntity, bool> validateFunc, string message)
        {
            Feature.AddRule(new CustomRule<TEntity>(validateFunc, message));
            return this;
        }

        public ObjectPropertyConfiguration<TEntity, TProp> HasSpecification(ISpecification<TEntity> specification)
        {
            return HasSpecification(specification, null);
        }

        public ObjectPropertyConfiguration<TEntity, TProp> HasSpecification(ISpecification<TEntity> specification, string message)
        {
            Feature.AddRule(new SpecificationRule<TEntity>(specification, message));
            return this;
        }
    }
}