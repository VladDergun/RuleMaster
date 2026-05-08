namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value matches the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern the value must match, e.g. <c>@"^\d{4}$"</c>.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RegularExpressionRule(string pattern) : PropertyRule
    {
        public string Pattern { get; } = pattern ?? throw new ArgumentNullException(nameof(pattern));

        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException($"Property '{propertyName}' cannot be null.", propertyName);
            }

            if (value is not string stringValue)
            {
                throw new RuleValidationException($"Property '{propertyName}' must be a string.", propertyName);
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(stringValue, Pattern))
            {
                throw new RuleValidationException($"Property '{propertyName}' does not match the required pattern.", propertyName);
            }
        }
    }
}
