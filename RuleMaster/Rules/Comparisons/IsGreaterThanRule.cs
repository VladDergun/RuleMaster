namespace RuleMaster.Rules.Comparisons
{
    /// <summary>
    /// Validates that the property value is strictly greater than the specified threshold.
    /// Works with any <see cref="IComparable"/> type including <see cref="int"/>, <see cref="double"/>, <see cref="DateTime"/>, and <see cref="string"/>.
    /// </summary>
    /// <param name="threshold">The exclusive lower bound the value must exceed.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsGreaterThanRule(IComparable threshold) : PropertyRule
    {
        public IComparable Threshold { get; } = threshold;

        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is IComparable comparable)
            {
                if (comparable.CompareTo(Threshold) <= 0)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be greater than '{Threshold}'.",
                        propertyName);
                }
            }
        }
    }
}
