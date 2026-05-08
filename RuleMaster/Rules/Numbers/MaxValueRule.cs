namespace RuleMaster.Rules.Numbers
{
    /// <summary>
    /// Validates that the numeric property value is less than or equal to the specified maximum.
    /// </summary>
    /// <param name="maxValue">The inclusive maximum allowed value.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaxValueRule(double maxValue) : PropertyRule
    {
        public double MaxValue { get; } = maxValue;

        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is IConvertible)
            {
                double numericValue = Convert.ToDouble(value);

                if (numericValue > MaxValue)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be less than or equal to {MaxValue}.",
                        propertyName);
                }
            }
        }
    }
}
