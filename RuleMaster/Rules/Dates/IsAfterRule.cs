namespace RuleMaster.Rules.Dates
{
    /// <summary>
    /// Validates that the <see cref="DateTime"/> property value is strictly after the specified date.
    /// </summary>
    /// <param name="date">The exclusive lower bound date in <c>yyyy-MM-dd</c> format, e.g. <c>"2020-01-31"</c>.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsAfterRule(string date) : PropertyRule
    {
        public DateTime Date { get; } = DateTime.ParseExact(date, "yyyy-MM-dd", null);

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
                if (dateTime <= Date)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be after {Date:yyyy-MM-dd}.",
                        propertyName);
                }
            }
        }
    }
}
