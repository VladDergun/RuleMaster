namespace RuleMaster.Rules.Comparisons
{
    /// <summary>
    /// Validates that the property value is not equal to the specified forbidden value.
    /// Null values are always considered valid.
    /// </summary>
    /// <param name="forbidden">The value the property must not equal.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotEqualToRule(object forbidden) : PropertyRule
    {
        public object Forbidden { get; } = forbidden;

        public override void Validate(string propertyName, object? value)
        {
            if (value != null && Forbidden.Equals(value))
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be equal to '{Forbidden}'.",
                    propertyName);
            }
        }
    }
}
