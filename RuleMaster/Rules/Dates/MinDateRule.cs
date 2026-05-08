namespace RuleMaster.Rules.Dates
{
    /// <summary>
    /// Validates that the <see cref="DateTime"/> property value is greater than or equal to the specified date.
    /// </summary>
    /// <param name="minDate">The inclusive minimum date in <c>yyyy-MM-dd</c> format, e.g. <c>"2000-01-01"</c>.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinDateRule(string minDate) : PropertyRule
    {
        public DateTime MinDate { get; } = DateTime.ParseExact(minDate, "yyyy-MM-dd", null);

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
                if (dateTime < MinDate)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be greater than or equal to {MinDate:yyyy-MM-dd}.",
                        propertyName);
                }
            }
        }
    }
}
