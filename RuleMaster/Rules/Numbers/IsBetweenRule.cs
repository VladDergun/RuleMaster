namespace RuleMaster.Rules.Numbers
{
    /// <summary>
    /// Validates that the numeric property value falls within an inclusive range.
    /// </summary>
    /// <param name="minValue">The inclusive minimum allowed value.</param>
    /// <param name="maxValue">The inclusive maximum allowed value.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsBetweenRule(double minValue, double maxValue) : PropertyRule
    {
        public double MinValue { get; } = minValue;
        public double MaxValue { get; } = maxValue;

        private void ThrowIfInvalid()
        {
            if (MinValue > MaxValue)
                throw new InvalidOperationException($"{nameof(IsBetweenRule)}: MinValue cannot be greater than MaxValue.");
        }

        public override void Validate(string propertyName, object? value)
        {
            ThrowIfInvalid();

            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is IConvertible)
            {
                double numericValue = Convert.ToDouble(value);
                if (numericValue < MinValue || numericValue > MaxValue)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be between {MinValue} and {MaxValue}.",
                        propertyName);
                }
            }
        }
    }
}
