using System.Text;

namespace RuleMaster
{
    /// <summary>
    /// Thrown by <see cref="RuleEnforcer"/> when one or more property rules fail validation.
    /// Contains all <see cref="RuleValidationException"/> errors collected during a full validation pass.
    /// </summary>
    /// <param name="errors">The list of individual validation errors that occurred.</param>
    public class AggregateRuleValidationException(IReadOnlyList<RuleValidationException> errors) 
        : Exception(BuildMessage(errors))
    {
        /// <summary>The individual validation errors collected during the validation pass.</summary>
        public IReadOnlyList<RuleValidationException> Errors { get; } = errors;

        private static string BuildMessage(IReadOnlyList<RuleValidationException> errors)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Validation failed with {errors.Count} error(s):");
            for (int i = 0; i < errors.Count; i++)
            {
                sb.AppendLine($"  [{i + 1}] {errors[i].Message}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
