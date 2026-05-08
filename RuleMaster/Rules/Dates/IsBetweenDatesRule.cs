namespace RuleMaster.Rules.Dates
{
    /// <summary>
    /// Validates that the <see cref="DateTime"/> property value falls within an inclusive date range.
    /// </summary>
    /// <param name="minDate">The inclusive lower bound date in <c>yyyy-MM-dd</c> format, e.g. <c>"2020-01-01"</c>.</param>
    /// <param name="maxDate">The inclusive upper bound date in <c>yyyy-MM-dd</c> format, e.g. <c>"2030-12-31"</c>.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsBetweenDatesRule(string minDate, string maxDate) : PropertyRule
    {
        public DateTime MinDate { get; } = DateTime.ParseExact(minDate, "yyyy-MM-dd", null);
        public DateTime MaxDate { get; } = DateTime.ParseExact(maxDate, "yyyy-MM-dd", null);

        private void ThrowIfInvalid()
        {
            if (MinDate > MaxDate)
                throw new InvalidOperationException($"{nameof(IsBetweenDatesRule)}: MinDate cannot be greater than MaxDate.");
        }

        public override void Validate(string propertyName, object? value)
        {
            ThrowIfInvalid();

            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is DateTime dateTime)
            {
                if (dateTime < MinDate || dateTime > MaxDate)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be between {MinDate:yyyy-MM-dd} and {MaxDate:yyyy-MM-dd}.",
                        propertyName);
                }
            }
        }
    }
}
