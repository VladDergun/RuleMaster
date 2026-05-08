namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value starts with the specified prefix.
    /// </summary>
    /// <param name="value">The required prefix string.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StartsWithRule(string value) : PropertyRule
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

            if (!stringValue.StartsWith(Value))
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' must start with '{Value}'.",
                    propertyName);
            }
        }
    }
}
