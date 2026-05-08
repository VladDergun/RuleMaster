using RuleMaster.Regexes;

namespace RuleMaster.Rules.Patterns
{
    /// <summary>
    /// Validates that the string property value is a valid phone number in E.164 international format.
    /// An optional leading <c>+</c> is allowed, followed by 7 to 15 digits with a non-zero first digit.
    /// Examples: <c>"+14155552671"</c>, <c>"441234567890"</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PhoneNumberRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is not string phone)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' must be a string.",
                    propertyName);
            }

            if (!GeneratedRegexes.PhoneNumber().IsMatch(phone))
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' is not a valid phone number.",
                    propertyName);
            }
        }
    }
}
