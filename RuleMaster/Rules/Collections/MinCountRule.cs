using RuleMaster;
using System.Collections;

namespace RuleMaster.Rules.Collections
{
    /// <summary>
    /// Validates that the collection property value contains at least the specified number of elements.
    /// </summary>
    /// <param name="minCount">The minimum required element count. Must be non-negative.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinCountRule(int minCount) : PropertyRule
    {
        public int MinCount { get; } = Guard.AgainstNegative(minCount);

        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is IEnumerable enumerable)
            {
                int count = 0;
                foreach (var _ in enumerable)
                    count++;

                if (count < MinCount)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must contain at least {MinCount} items.",
                        propertyName);
                }
            }
        }
    }
}
