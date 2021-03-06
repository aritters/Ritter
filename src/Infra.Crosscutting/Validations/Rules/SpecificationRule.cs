using Ritter.Infra.Crosscutting.Specifications;

namespace Ritter.Infra.Crosscutting.Validations.Rules
{
    public sealed class SpecificationRule<TValidable> : ValidationRule<TValidable> where TValidable : class
    {
        public ISpecification<TValidable> Rule { get; private set; }

        public SpecificationRule(ISpecification<TValidable> rule) : this(rule, null) { }

        public SpecificationRule(ISpecification<TValidable> rule, string message) : base(message)
        {
            Ensure.Argument.NotNull(rule, nameof(rule));
            Rule = rule;
        }

        public override bool IsValid(TValidable entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity), "Expected a valid non-null entity instance against which the rule can be evaluated.");
            return Rule.IsSatisfiedBy(entity);
        }
    }
}
