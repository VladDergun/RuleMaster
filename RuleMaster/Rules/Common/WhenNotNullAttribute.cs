namespace RuleMaster.Rules.Common
{
    /// <summary>
    /// Skips all <see cref="PropertyRule"/> attributes listed below this attribute when the property value is <c>null</c>.
    /// Place above any rules that should only run on non-null values.
    /// </summary>
    /// <example>
    /// <code>
    /// [WhenNotNull]
    /// [MinLength(5)]
    /// [MaxLength(100)]
    /// public string? OptionalName { get; set; }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class WhenNotNullAttribute : ConditionalEnforcementAttribute
    {
        /// <inheritdoc/>
        public override bool ShouldEnforce(object? value, object entity) => value is not null;
    }
}
