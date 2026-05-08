namespace RuleMaster.Rules.Dates
{
    /// <summary>
    /// Validates that the <see cref="DateTime"/> property value represents a date of birth at least <paramref name="years"/> years ago.
    /// </summary>
    /// <param name="years">The minimum required age in whole years, e.g. <c>18</c> for adult validation.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AgeAtLeastRule(int years) : PropertyRule
    {
        public int Years { get; } = years;

        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is DateTime dateOfBirth)
            {
                var today = DateTime.UtcNow;
                int age = today.Year - dateOfBirth.Year;
                if (dateOfBirth.Date > today.AddYears(-age))
                    age--;

                if (age < Years)
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must represent an age of at least {Years} years.",
                        propertyName);
                }
            }
        }
    }
}
