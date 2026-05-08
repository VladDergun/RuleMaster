namespace RuleMaster.Rules.Common
{
    /// <summary>
    /// Validates that the enum property value is a defined member of its enum type.
    /// Useful for catching out-of-range numeric casts, e.g. <c>(OrderStatus)99</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DefinedEnumValueRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            Type type = value.GetType();

            if (!type.IsEnum)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' must be an enum type.",
                    propertyName);
            }

            if (!Enum.IsDefined(type, value))
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' has value '{value}' which is not defined in enum '{type.Name}'.",
                    propertyName);
            }
        }
    }
}
