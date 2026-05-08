namespace RuleMaster
{
    /// <summary>
    /// Represents a validation failure for a single property rule.
    /// Thrown by individual <see cref="Rules.PropertyRule"/> implementations and collected by <see cref="RuleEnforcer"/>.
    /// </summary>
    /// <param name="message">A description of why validation failed.</param>
    /// <param name="propertyName">The name of the property that failed validation.</param>
    public class RuleValidationException(string message, string propertyName) : Exception(message)
    {
        /// <summary>The name of the property that failed validation.</summary>
        public string PropertyName { get; } = propertyName;

        /// <inheritdoc/>
        public override string Message =>
            $"Validation failed for property '{PropertyName}': {base.Message}";
    }
}
