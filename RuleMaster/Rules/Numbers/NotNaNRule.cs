namespace RuleMaster.Rules.Numbers
{
    /// <summary>
    /// Validates that the <see cref="double"/> or <see cref="float"/> property value is not <see cref="double.NaN"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotNaNRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is double d && double.IsNaN(d))
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be NaN.",
                    propertyName);
            }

            if (value is float f && float.IsNaN(f))
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be NaN.",
                    propertyName);
            }
        }
    }
}
