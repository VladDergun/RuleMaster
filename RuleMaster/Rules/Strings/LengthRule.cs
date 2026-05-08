using RuleMaster;

namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value length falls within an inclusive range.
    /// </summary>
    /// <param name="minLength">The minimum required character count. Must be non-negative.</param>
    /// <param name="maxLength">The maximum permitted character count. Must be non-negative and greater than or equal to <paramref name="minLength"/>.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class LengthRule(int minLength, int maxLength) : PropertyRule
    {
        public int MinLength { get; } = Guard.AgainstNegative(minLength);
        public int MaxLength { get; } = Guard.AgainstNegative(maxLength);

        public override void Validate(string propertyName, object? value)
        {
            if(MinLength > MaxLength)
            {
                throw new RuleValidationException("Minimal length cannot be greater than maximal length.", propertyName);
            }

            if (value is string strValue)
            {
                if (strValue.Length < MinLength || strValue.Length > MaxLength)
                {
                    throw new RuleValidationException($"Property '{propertyName}' must be between {MinLength} and {MaxLength} characters long.", propertyName);
                }
            }
        }
    }
}
