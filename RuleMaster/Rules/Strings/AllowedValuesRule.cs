namespace RuleMaster.Rules.Strings
{
    /// <summary>
    /// Validates that the string property value is one of the specified allowed values.
    /// </summary>
    /// <param name="allowedValues">The set of permitted string values, e.g. <c>"Pending", "Active", "Cancelled"</c>.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AllowedValuesRule(params string[] allowedValues) : PropertyRule
    {
        public IReadOnlyList<string> AllowedValues { get; } = allowedValues;

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
                if (!AllowedValues.Contains(strValue))
                {
                    var allowed = string.Join(", ", AllowedValues.Select(v => $"'{v}'"));
                    throw new RuleValidationException(
                        $"Property '{propertyName}' must be one of the following values: {allowed}.",
                        propertyName);
                }
            }
        }
    }
}
