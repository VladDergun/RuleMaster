namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value consists entirely of lowercase characters.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsLowerCaseRule : PropertyRule
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
                if (strValue != strValue.ToLowerInvariant())
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be lowercase.",
                        propertyName);
                }
            }
        }
    }
}
