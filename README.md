Validation
==========

*Method input validation and runtime checks that report errors or throw
exceptions when failures are detected.*

[![Build status](https://ci.appveyor.com/api/projects/status/nhrah957le2jri3q/branch/master?svg=true)](https://ci.appveyor.com/project/AArnott/validation/branch/master)
[![NuGet package](https://img.shields.io/nuget/v/Validation.svg)](https://nuget.org/packages/Validation)

This project is available as the [Validation][1] NuGet package.

Basic input validation via the `Requires` class throws an ArgumentException.

```cs
Requires.NotNull(arg1, "arg1");
Requires.NotNullOrEmpty(arg2, "arg2");
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
