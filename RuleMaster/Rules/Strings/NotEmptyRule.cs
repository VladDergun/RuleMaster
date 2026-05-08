namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value is not null or empty. Whitespace-only strings are allowed.
    /// Use <see cref="NotWhitespaceRule"/> to also reject whitespace-only strings.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotEmptyRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException($"Property '{propertyName}' cannot be null.", propertyName);
            }

            if (value is string strValue)
            {
                if (string.IsNullOrEmpty(strValue))
                {
                    throw new RuleValidationException($"Property '{propertyName}' cannot be empty.", propertyName);
                }
            }
        }
    }
}

