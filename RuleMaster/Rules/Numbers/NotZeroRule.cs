namespace RuleMaster.Rules.Numbers
{
    /// <summary>
    /// Validates that the numeric property value is not equal to zero.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotZeroRule : PropertyRule
    {
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
                if (numericValue == 0)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' cannot be zero.",
                        propertyName);
                }
            }
        }
    }
}
