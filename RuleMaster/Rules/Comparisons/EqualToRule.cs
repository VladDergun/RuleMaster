namespace RuleMaster.Rules.Comparisons
{
    /// <summary>
    /// Validates that the property value is equal to the specified expected value.
    /// Null values are treated as not equal.
    /// </summary>
    /// <param name="expected">The value the property must equal.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EqualToRule(object expected) : PropertyRule
    {
        public object Expected { get; } = expected;

        public override void Validate(string propertyName, object? value)
        {
            if (value == null || !Expected.Equals(value))
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' must be equal to '{Expected}'.",
                    propertyName);
            }
        }
    }
}
