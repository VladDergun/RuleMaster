namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value is non-empty and contains only digit characters (0-9).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsNumericStringRule : PropertyRule
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
                if (strValue.Length == 0 || !strValue.All(char.IsDigit))
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must contain only digit characters.",
                        propertyName);
                }
            }
        }
    }
}
