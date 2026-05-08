using RuleMaster;

namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value does not exceed the specified number of characters.
    /// </summary>
    /// <param name="maxLength">The maximum permitted character count. Must be non-negative.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class MaxLengthRule(int maxLength) : PropertyRule
    {
        public int MaxLength { get; } = Guard.AgainstNegative(maxLength);

        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException($"Property '{propertyName}' cannot be null.", propertyName);
            }

            if(value is string strValue)
            {
                if (strValue.Length > MaxLength)
                {
                    throw new RuleValidationException($"Property '{propertyName}' cannot exceed {MaxLength} characters.", propertyName);
                }
            }
        }
    }
}
