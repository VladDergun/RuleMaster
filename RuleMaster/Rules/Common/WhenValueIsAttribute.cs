namespace RuleMaster.Rules.Common
{
    /// <summary>
    /// Skips all <see cref="PropertyRule"/> attributes listed below this attribute
    /// unless the property value equals <typeparamref name="T"/> <paramref name="expectedValue"/>.
    /// Can be stacked with other conditional enforcement attributes; all conditions must pass.
    /// </summary>
    /// <typeparam name="T">The type of the expected value.</typeparam>
    /// <param name="expectedValue">The value the property must equal for enforcement to continue.</param>
    /// <example>
    /// <code>
    /// [WhenValueIs&lt;string&gt;("active")]
    /// [MinLength(5)]
    /// public string Status { get; set; }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class WhenValueIsAttribute<T>(T expectedValue) : ConditionalEnforcementAttribute
    {
        /// <summary>The value the property must equal for rules below to be enforced.</summary>
        public T ExpectedValue { get; } = expectedValue;

        /// <inheritdoc/>
        public override bool ShouldEnforce(object? value, object entity)
            => value is T typed && EqualityComparer<T>.Default.Equals(typed, ExpectedValue);
    }
}
