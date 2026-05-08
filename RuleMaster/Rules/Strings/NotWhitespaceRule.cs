namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value is not null, empty, or whitespace-only.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotWhitespaceRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is string strValue)
            {
                if (string.IsNullOrWhiteSpace(strValue))
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' cannot be empty or whitespace.",
                        propertyName);
                }
            }
        }
    }
}
