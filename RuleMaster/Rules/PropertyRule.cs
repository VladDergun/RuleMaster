using System;
namespace RuleMaster.Rules
{
    /// <summary>
    /// Base class for all property validation rules. Derive from this class to implement a custom rule.
    /// Rules are applied as attributes on entity properties and evaluated by <see cref="RuleMaster.RuleEnforcer"/>.
    /// </summary>
    public abstract class PropertyRule : Attribute
    {
        /// <summary>
        /// Validates the property value. Throws <see cref="RuleMaster.RuleValidationException"/> if validation fails.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="value">The current value of the property.</param>
        public abstract void Validate(string propertyName, object? value);

        /// <summary>
        /// Validates the property value with access to the owning entity.
        /// Override this when the rule needs to inspect sibling properties.
        /// By default, delegates to <see cref="Validate(string, object?)"/>.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="value">The current value of the property.</param>
        /// <param name="entity">The entity instance that owns the property.</param>
        public virtual void Validate(string propertyName, object? value, object entity)
            => Validate(propertyName, value);
    }
}
