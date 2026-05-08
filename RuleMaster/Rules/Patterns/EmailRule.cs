using System.Net.Mail;

namespace RuleMaster.Rules.Patterns
{
    /// <summary>
    /// Validates that the string property value is a well-formed email address.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EmailRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is not string email)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' must be a string.",
                    propertyName);
            }

            try
            {
                var address = new MailAddress(email);

                if (address.Address != email)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' is not a valid email address.",
                        propertyName);
                }
            }
            catch (FormatException)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' is not a valid email address.",
                    propertyName);
            }
        }
    }
}
