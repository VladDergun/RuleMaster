using System.Collections;

namespace RuleMaster.Rules.Collections
{
    /// <summary>
    /// Validates that the collection property value contains no duplicate elements.
    /// Null elements are ignored during the duplicate check.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoDuplicatesRule : PropertyRule
    {
        public override void Validate(string propertyName, object? value)
        {
            if (value == null)
            {
                throw new RuleValidationException(
                    $"Property '{propertyName}' cannot be null.",
                    propertyName);
            }

            if (value is IEnumerable enumerable)
            {
                var seen = new HashSet<object>();
                foreach (var item in enumerable)
                {
                    if (item != null && !seen.Add(item))
                    {
                        throw new RuleValidationException(
                            $"Property '{propertyName}' must not contain duplicate values.",
                            propertyName);
                    }
                }
            }
        }
    }
}
