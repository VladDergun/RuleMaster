using RuleMaster;

namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value is at least the specified number of characters long.
    /// </summary>
    /// <param name="minLength">The minimum required character count. Must be non-negative.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class MinLengthRule(int minLength) : PropertyRule
    {
        public int MinLength { get; } = Guard.AgainstNegative(minLength);

        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException($"Property '{propertyName}' cannot be null.", propertyName);
            }

            if (value is string strValue)
            {
                if (strValue.Length < MinLength)
                {
                    throw new RuleValidationException($"Property '{propertyName}' must be at least {MinLength} characters long.", propertyName);
                }
            }
        }
    }
}
