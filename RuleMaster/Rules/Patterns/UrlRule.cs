namespace RuleMaster.Rules.Patterns
{
    /// <summary>
    /// Validates that the string property value is a well-formed absolute URL using the HTTP or HTTPS scheme.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UrlRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is not string url)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' must be a string.",
                    propertyName);
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' is not a valid URL.",
                    propertyName);
            }

            if (uri.Scheme != Uri.UriSchemeHttp &&
                uri.Scheme != Uri.UriSchemeHttps)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' must use HTTP or HTTPS.",
                    propertyName);
            }
        }
    }
}
