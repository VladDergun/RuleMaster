using System;
using System.Collections.Generic;
using System.Text;

namespace RuleMaster
{
    internal sealed class Guard
    {
        internal static int AgainstNegative(int value, string? parameterName = null)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(parameterName, "Value cannot be negative.");
            }
            return value;
        }
    }
}
