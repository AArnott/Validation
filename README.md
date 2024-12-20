Validation
==========

*Method input validation and runtime checks that report errors or throw
exceptions when failures are detected.*

[![NuGet package](https://img.shields.io/nuget/v/Validation.svg)](https://nuget.org/packages/Validation)
[![🏭 Build](https://github.com/AArnott/Validation/actions/workflows/build.yml/badge.svg)](https://github.com/AArnott/Validation/actions/workflows/build.yml)
[![codecov](https://codecov.io/gh/AArnott/Validation/branch/main/graph/badge.svg)](https://codecov.io/gh/AArnott/Validation)

This project is available as the [Validation][1] NuGet package.

[Check out our full documentation](https://aarnott.github.io/Validation).

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

[1]: http://nuget.org/packages/Validation "Validation NuGet package"
