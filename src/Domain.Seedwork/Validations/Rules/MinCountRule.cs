using Ritter.Infra.Crosscutting;
using Ritter.Infra.Crosscutting.Extensions;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace Ritter.Domain.Validations.Rules
{
    public sealed class MinCountRule<TValidable> : PropertyRule<TValidable, ICollection> where TValidable : class
    {
        private readonly int minCount;

        public MinCountRule(Expression<Func<TValidable, ICollection>> expression, int minCount) : this(expression, minCount, null) { }

        public MinCountRule(Expression<Func<TValidable, ICollection>> expression, int minCount, string message) : base(expression, message)
        {
            Ensure.Argument.NotNull(minCount, nameof(minCount));
            this.minCount = minCount;
        }

        public override bool Validate(TValidable entity)
        {
            ICollection collection = Compile(entity);

            if (collection.IsNull() && minCount > 0)
                return false;

            return collection.Count >= minCount;
        }
    }
}
