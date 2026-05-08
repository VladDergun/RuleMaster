namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value contains the specified substring.
    /// </summary>
    /// <param name="value">The required substring.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ContainsRule(string value) : PropertyRule
    {
        public string Value { get; } = value;

        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is not string stringValue)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' must be a string.",
                    propertyName);
            }

            if (!stringValue.Contains(Value))
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' must contain '{Value}'.",
                    propertyName);
            }
        }
    }
}
