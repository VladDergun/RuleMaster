namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value ends with the specified suffix.
    /// </summary>
    /// <param name="value">The required suffix string.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EndsWithRule(string value) : PropertyRule
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

            if (!stringValue.EndsWith(Value))
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' must end with '{Value}'.",
                    propertyName);
            }
        }
    }
}
