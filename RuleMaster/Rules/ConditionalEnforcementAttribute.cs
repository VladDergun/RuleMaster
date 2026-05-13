namespace RuleMaster.Rules
{
    /// <summary>
    /// Base class for conditional enforcement attributes.
    /// When placed above <see cref="PropertyRule"/> attributes on a property, controls whether
    /// the rules below it are enforced based on the property's current value.
    /// Multiple conditional attributes accumulate — all conditions must pass for rules below them to run.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class ConditionalEnforcementAttribute : Attribute
    {
        /// <summary>
        /// Determines whether rules below this attribute should be enforced.
        /// </summary>
        /// <param name="value">The current value of the property.</param>
        /// <param name="entity">The entity instance that owns the property.</param>
        /// <returns><c>true</c> if enforcement should continue; otherwise <c>false</c>.</returns>
        public abstract bool ShouldEnforce(object? value, object entity);
    }
}
