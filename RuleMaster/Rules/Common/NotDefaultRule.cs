namespace RuleMaster.Rules.Common
{
    /// <summary>
    /// Validates that the value type property value is not the default for its type
    /// (e.g. <c>0</c> for <see cref="int"/>, <see cref="Guid.Empty"/> for <see cref="Guid"/>, <see cref="DateTime.MinValue"/> for <see cref="DateTime"/>).
    /// Has no effect on reference types — use <see cref="RequiredRule"/> for those.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotDefaultRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            var type = value.GetType();
            if (type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                if (value.Equals(defaultValue))
                {
                    throw new RuleValidationException(
                        $"Property '{propertyName}' cannot be the default value for type '{type.Name}'.",
                        propertyName);
                }
            }
        }
    }
}
