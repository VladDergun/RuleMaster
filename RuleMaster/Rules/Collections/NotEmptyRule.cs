namespace RuleMaster.Rules.Collections
{
    /// <summary>
    /// Validates that the collection property value is not null and contains at least one element.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotEmptyRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if(value == null)
            {
                throw new RuleValidationException($"Property '{propertyName}' cannot be null.", propertyName);
            }

            if(value is System.Collections.IEnumerable enumerable)
            {
                if(!enumerable.GetEnumerator().MoveNext())
                {
                    throw new RuleValidationException($"Property '{propertyName}' cannot be an empty collection.", propertyName);
                }
            }
        }
    }
}
