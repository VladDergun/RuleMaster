namespace RuleMaster.Rules.Common
{
    /// <summary>
    /// Skips all <see cref="PropertyRule"/> attributes listed below this attribute
    /// when the property value equals <typeparamref name="T"/> <paramref name="excludedValue"/>.
    /// Can be stacked with other conditional enforcement attributes; all conditions must pass.
    /// </summary>
    /// <typeparam name="T">The type of the excluded value.</typeparam>
    /// <param name="excludedValue">The value that, when matched, causes rules below to be skipped.</param>
    /// <example>
    /// <code>
    /// [WhenValueIsNot&lt;string&gt;("N/A")]
    /// [MinLength(5)]
    /// public string Code { get; set; }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class WhenValueIsNotAttribute<T>(T excludedValue) : ConditionalEnforcementAttribute
    {
        /// <summary>The value that disables enforcement of rules below when matched.</summary>
        public T ExcludedValue { get; } = excludedValue;

        /// <inheritdoc/>
        public override bool ShouldEnforce(object? value, object entity)
            => !(value is T typed && EqualityComparer<T>.Default.Equals(typed, ExcludedValue));
    }
}
