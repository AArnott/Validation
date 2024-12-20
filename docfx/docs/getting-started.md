# Getting Started

## Installation

Consume this library via its NuGet Package.
Click on the badge to find its latest version and the instructions for consuming it that best apply to your project.

[![NuGet package](https://img.shields.io/nuget/v/Validation.svg)](https://nuget.org/packages/Validation)

## Usage

Bring the classes into scope with this `using` at the start of your code file:

```cs
using Validation;
```

Basic input validation via the `Requires` class throws an ArgumentException.

```cs
Requires.NotNull(arg1);
Requires.NotNullOrEmpty(arg2);
```

State validation via the `Verify` class throws an InvalidOperationException.

```csharp
Verify.Operation(condition, "some error occurred.");
```

Internal integrity checks via the `Assumes` class throws an
InternalErrorException.

```csharp
Assumes.True(condition, "some error");
```

Warning signs that should not throw exceptions via the `Report` class.

```csharp
Report.IfNot(condition, "some error");
```
