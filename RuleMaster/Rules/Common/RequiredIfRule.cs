using System.Reflection;

namespace RuleMaster.Rules.Common
{
    /// <summary>
    /// Validates that the property value is not null when another property on the same entity equals a specified value.
    /// Can be applied multiple times on the same property for multiple conditions.
    /// </summary>
    /// <param name="conditionPropertyName">The name of the sibling property to check, e.g. <c>nameof(OrderType)</c>.</param>
    /// <param name="conditionValue">The value the sibling property must equal to trigger the requirement.</param>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredIfRule(string conditionPropertyName, object conditionValue) : PropertyRule
    {
        public string ConditionPropertyName { get; } = conditionPropertyName;
        public object ConditionValue { get; } = conditionValue;

        public override void Validate(string propertyName, object? value)
            => throw new InvalidOperationException(
                $"{nameof(RequiredIfRule)} requires access to the entity. Use the Validate overload that accepts an entity.");

        public override void Validate(string propertyName, object? value, object entity)
        {
            PropertyInfo? conditionProperty = entity.GetType().GetProperty(ConditionPropertyName);

            if (conditionProperty == null)
            {
                throw new InvalidOperationException(
                    $"Property '{ConditionPropertyName}' does not exist on type '{entity.GetType().Name}'.");
            }

            object? conditionPropertyValue = conditionProperty.GetValue(entity);

            if (Equals(conditionPropertyValue, ConditionValue) && value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' is required when '{ConditionPropertyName}' is '{ConditionValue}'.",
                    propertyName);
            }
        }
    }
}
