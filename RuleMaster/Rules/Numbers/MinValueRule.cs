using RuleMaster;
using RuleMaster.Rules;

namespace RuleMaster.Rules.Numbers
{
    /// <summary>
    /// Validates that the numeric property value is greater than or equal to the specified minimum.
    /// </summary>
    /// <param name="minValue">The inclusive minimum allowed value.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinValueRule(double minValue) : PropertyRule
    {
        public double MinValue { get; } = minValue;

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

                if (numericValue < MinValue)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be greater than or equal to {MinValue}.",
                        propertyName);
                }
            }
        }
    }
}
