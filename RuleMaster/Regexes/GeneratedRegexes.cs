using System.Text.RegularExpressions;

namespace RuleMaster.Regexes
{
    internal static partial class GeneratedRegexes
    {
        [GeneratedRegex(@"^\+?[1-9]\d{6,14}$")]
        internal static partial Regex PhoneNumber();

        [GeneratedRegex(@"^[a-zA-Z0-9]*$")]
        internal static partial Regex AlphanumericOnly();
    }
}
