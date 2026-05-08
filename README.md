# RuleMaster

A powerful, attribute-based validation library for .NET that provides comprehensive rule enforcement with clear error messages and fluent API support.

## Features

- **Attribute-based validation**: Decorate your properties with validation attributes
- **Comprehensive rule set**: Built-in rules for strings, numbers, dates, collections, and more
- **Clear error messages**: Detailed validation exceptions with property names
- **Fluent API support**: Works seamlessly with existing validation frameworks
- **Performance optimized**: Minimal overhead with efficient validation logic
- **.NET 10.0 support**: Built on the latest .NET platform

## Installation

```bash
dotnet add package RuleMaster
```

## Quick Start

```csharp
using RuleMaster;

public class User
{
    [RequiredRule]
    [NotEmptyRule]
    [MinLengthRule(3)]
    public string Name { get; set; }

    [RequiredRule]
    [EmailRule]
    public string Email { get; set; }

    [MinValueRule(18)]
    [MaxValueRule(120)]
    public int Age { get; set; }

    [RequiredRule]
    [IsInFutureRule]
    public DateTime BirthDate { get; set; }
}

// Validate your entities
var user = new User { Name = "John", Email = "john@example.com", Age = 25, BirthDate = DateTime.Now.AddDays(1) };

try
{
    RuleEnforcer.EnforceRules(user);
    Console.WriteLine("Validation passed!");
}
catch (AggregateRuleValidationException ex)
{
    foreach (var error in ex.Errors)
    {
        Console.WriteLine($"Error: {error.Message}");
    }
}
```

## Available Rules

### String Rules
- `RequiredRule` - Validates that the property is not null
- `NotEmptyRule` - Validates that the string is not null or empty
- `MinLengthRule(int minLength)` - Validates minimum string length
- `MaxLengthRule(int maxLength)` - Validates maximum string length
- `LengthRule(int exactLength)` - Validates exact string length
- `ExactLengthRule(int exactLength)` - Validates exact string length
- `IsUpperCaseRule` - Validates that string is uppercase
- `IsLowerCaseRule` - Validates that string is lowercase
- `IsNumericStringRule` - Validates that string contains only numbers
- `NoSpecialCharactersRule` - Validates that string contains only alphanumeric characters
- `NotWhitespaceRule` - Validates that string is not just whitespace
- `StartsWithRule(string prefix)` - Validates that string starts with prefix
- `EndsWithRule(string suffix)` - Validates that string ends with suffix
- `ContainsRule(string substring)` - Validates that string contains substring
- `AllowedValuesRule(params string[] allowedValues)` - Validates against allowed values
- `RegularExpressionRule(string pattern)` - Validates with custom regex pattern

### Number Rules
- `NotZeroRule` - Validates that number is not zero
- `NotNegativeRule` - Validates that number is not negative
- `NotNaNRule` - Validates that double is not NaN
- `MinValueRule(double minValue)` - Validates minimum value
- `MaxValueRule(double maxValue)` - Validates maximum value
- `IsPositiveRule` - Validates that number is positive
- `IsNegativeRule` - Validates that number is negative
- `IsBetweenRule(double min, double max)` - Validates that number is in range

### Date Rules
- `MinDateRule(DateTime minDate)` - Validates minimum date
- `MaxDateRule(DateTime maxDate)` - Validates maximum date
- `IsInPastRule` - Validates that date is in the past
- `IsInFutureRule` - Validates that date is in the future
- `IsBeforeRule(DateTime date)` - Validates that date is before specified date
- `IsAfterRule(DateTime date)` - Validates that date is after specified date
- `IsBetweenDatesRule(DateTime min, DateTime max)` - Validates that date is in range
- `IsNotWeekendRule` - Validates that date is not a weekend
- `AgeAtLeastRule(int years)` - Validates that date represents minimum age

### Pattern Rules
- `EmailRule` - Validates email format
- `PhoneNumberRule` - Validates phone number in E.164 format
- `UrlRule` - Validates URL format

### Collection Rules
- `NotEmptyRule` - Validates that collection is not empty
- `MinCountRule(int minCount)` - Validates minimum collection count
- `MaxCountRule(int maxCount)` - Validates maximum collection count
- `NoDuplicatesRule` - Validates that collection has no duplicates

### Common Rules
- `RequiredRule` - Validates that property is not null
- `NotDefaultRule` - Validates that property is not default value
- `RequiredIfRule(string propertyName, object value)` - Validates conditionally
- `DefinedEnumValueRule` - Validates that enum value is defined

### Comparison Rules
- `EqualToRule(object value)` - Validates equality
- `NotEqualToRule(object value)` - Validates inequality
- `IsGreaterThanRule(object value)` - Validates greater than
- `IsLessThanRule(object value)` - Validates less than

## Custom Rules

Create custom validation rules by inheriting from `PropertyRule`:

```csharp
public class CustomRule : PropertyRule
{
    public override void Validate(string propertyName, object? value)
    {
        if (value == null)
        {
            throw new RuleValidationException($"Property '{propertyName}' cannot be null.", propertyName);
        }

        // Your validation logic here
        if (!IsValid(value))
        {
            throw new RuleValidationException($"Property '{propertyName}' failed custom validation.", propertyName);
        }
    }

    private bool IsValid(object value)
    {
        // Implement your validation logic
        return true;
    }
}
```

## Exception Handling

RuleMaster provides two main exception types:

- `RuleValidationException` - Thrown when a single rule fails
- `AggregateRuleValidationException` - Contains multiple validation errors

## Performance

RuleMaster is optimized for performance with:
- Minimal reflection overhead
- Efficient validation logic
- Support for validation caching
- No external dependencies for core functionality

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

If you have any issues or questions, please file an issue on our GitHub repository.
