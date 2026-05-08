using RuleMaster;

namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value is exactly the specified number of characters long.
    /// </summary>
    /// <param name="length">The required character count. Must be non-negative.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExactLengthRule(int length) : PropertyRule
    {
        public int Length { get; } = Guard.AgainstNegative(length);

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
                if (strValue.Length != Length)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be exactly {Length} characters long.",
                        propertyName);
                }
            }
        }
    }
}
