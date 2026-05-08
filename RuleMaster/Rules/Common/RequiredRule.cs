namespace RuleMaster.Rules.Common
{
    /// <summary>
    /// Validates that the property value is not null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException($"Property '{propertyName}' is required and cannot be null.", propertyName);
            }
        }
    }
}
