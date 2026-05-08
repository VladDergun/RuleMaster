using System.Reflection;

namespace RuleMaster
{
    /// <summary>
    /// Inspects all properties of an entity, executes any <see cref="Rules.PropertyRule"/> attributes found,
    /// and throws an <see cref="AggregateRuleValidationException"/> containing all failures if any rules are violated.
    /// </summary>
    public static class RuleEnforcer
    {
        /// <summary>
        /// Validates all rule-annotated properties on <paramref name="entity"/>.
        /// All rules are evaluated before throwing, so every violation is reported at once.
        /// </summary>
        /// <typeparam name="T">The type of the entity to validate.</typeparam>
        /// <param name="entity">The entity instance to validate. Must not be null.</param>
        /// <exception cref="AggregateRuleValidationException">Thrown when one or more rules fail.</exception>
        public static void EnforceRules<T>(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var properties = typeof(T).GetProperties();
            var errors = new List<RuleValidationException>();

            foreach (var property in properties)
            {
                var value = property.GetValue(entity);

                var rules = property
                    .GetCustomAttributes<Rules.PropertyRule>(false);

                foreach (var rule in rules)
                {
                    try
                    {
                        rule.Validate(property.Name, value, entity);
                    }
                    catch (RuleValidationException ex)
                    {
                        errors.Add(ex);
                    }
                }
            }

            if (errors.Count > 0)
            {
                throw new AggregateRuleValidationException(errors);
            }
        }
    }
}