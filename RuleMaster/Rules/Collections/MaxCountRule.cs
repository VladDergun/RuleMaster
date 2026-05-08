using RuleMaster;
using System.Collections;

namespace RuleMaster.Rules.Collections
{
    /// <summary>
    /// Validates that the collection property value contains at most the specified number of elements.
    /// </summary>
    /// <param name="maxCount">The maximum permitted element count. Must be non-negative.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaxCountRule(int maxCount) : PropertyRule
    {
        public int MaxCount { get; } = Guard.AgainstNegative(maxCount);

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

                if (count > MaxCount)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must contain at most {MaxCount} items.",
                        propertyName);
                }
            }
        }
    }
}
