using System.Collections.Concurrent;
using System.Reflection;

namespace RuleMaster
{
    /// <summary>
    /// Inspects all properties of an entity, executes any <see cref="Rules.PropertyRule"/> attributes found,
    /// and throws an <see cref="AggregateRuleValidationException"/> containing all failures if any rules are violated.
    /// </summary>
    public static class RuleEnforcer
    {
        private sealed record PropertyDescriptor(PropertyInfo Property, object[] Attributes);

        private static readonly ConcurrentDictionary<Type, PropertyDescriptor[]> _cache = new();

        private static PropertyDescriptor[] BuildDescriptors(Type type) =>
            type.GetProperties()
                .Select(p => new PropertyDescriptor(p, p.GetCustomAttributes(false)))
                .Where(d => d.Attributes.Any(a => a is Rules.PropertyRule or Rules.ConditionalEnforcementAttribute))
                .ToArray();

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

            var descriptors = _cache.GetOrAdd(entity.GetType(), BuildDescriptors);
            var errors = new List<RuleValidationException>();

            foreach (var (property, attributes) in descriptors)
            {
                var value = property.GetValue(entity);

                bool enforcing = true;

                foreach (var attr in attributes)
                {
                    if (attr is Rules.ConditionalEnforcementAttribute condition)
                    {
                        enforcing = enforcing && condition.ShouldEnforce(value, entity);
                    }
                    else if (attr is Rules.PropertyRule rule && enforcing)
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
            }

            if (errors.Count > 0)
            {
                throw new AggregateRuleValidationException(errors);
            }
        }
    }
}