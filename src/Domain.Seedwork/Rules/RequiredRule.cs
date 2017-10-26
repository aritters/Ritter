using Ritter.Infra.Crosscutting.Extensions;
using System;
using System.Linq.Expressions;

namespace Ritter.Domain.Seedwork.Rules
{
    public sealed class RequiredRule<TEntity, TProp> : PropertyRule<TEntity, TProp>
        where TEntity : class
    {
        public RequiredRule(Expression<Func<TEntity, TProp>> expression)
            : this(expression, null)
        {
        }

        public RequiredRule(Expression<Func<TEntity, TProp>> expression, string message)
            : base(expression, message)
        {
        }

        public override bool Validate(TEntity entity)
        {
            if (typeof(TProp) == typeof(string))
                return !(Compile(entity) as string).IsNullOrEmpty();

            return Compile(entity) != null;
        }
    }
}
