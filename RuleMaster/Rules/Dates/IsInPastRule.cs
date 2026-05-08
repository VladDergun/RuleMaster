namespace RuleMaster.Rules.Dates
{
    /// <summary>
    /// Validates that the <see cref="DateTime"/> property value is strictly before the current UTC time.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsInPastRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is DateTime dateTime)
            {
                if (dateTime >= DateTime.UtcNow)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be a date in the past.",
                        propertyName);
                }
            }
        }
    }
}
