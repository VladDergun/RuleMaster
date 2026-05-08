namespace RuleMaster.Rules.Dates
{
    /// <summary>
    /// Validates that the <see cref="DateTime"/> property value is less than or equal to the specified date.
    /// </summary>
    /// <param name="maxDate">The inclusive maximum date in <c>yyyy-MM-dd</c> format, e.g. <c>"2099-12-31"</c>.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaxDateRule(string maxDate) : PropertyRule
    {
        public DateTime MaxDate { get; } = DateTime.ParseExact(maxDate, "yyyy-MM-dd", null);

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
                if (dateTime > MaxDate)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be less than or equal to {MaxDate:yyyy-MM-dd}.",
                        propertyName);
                }
            }
        }
    }
}
