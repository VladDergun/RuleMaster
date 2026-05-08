using RuleMaster.Regexes;

namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value contains only alphanumeric characters (A-Z, a-z, 0-9).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoSpecialCharactersRule : PropertyRule
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
                if (!GeneratedRegexes.AlphanumericOnly().IsMatch(strValue))
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must contain only alphanumeric characters.",
                        propertyName);
                }
            }
        }
    }
}
