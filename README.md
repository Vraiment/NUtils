[![Build Status](https://travis-ci.com/Vraiment/NUtils.svg?branch=master)](https://travis-ci.com/Vraiment/NUtils)

# NUtils
--------

NUtils is a library for C# projects to add useful classes and methods to prevent writing boilerplate.

## Extension methods

There is a big set of extesion methods to allow coding in a more functional way:

```C#
int[] numbers = // get your numbers..

var value = myNumbers
    .Where(i => i < 10)
    .TakeIf(numbers => numbers.Length > 10)
    ?.GetOrElse(0, NOT_FOUND)
    ?? RESPONSE_TOO_LONG;
```

## Validations

Provide shorthand validations, specially useful for arguments:

```C#
void Parse(string value)
{
    Validate.ArgumentNotNull(value, nameof(value));
    Validate.Argument(value.Length != 0, "Input value should not be empty.");

    // Actual parse logic...
}
```
