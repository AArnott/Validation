# Validation

*Method input validation and runtime checks that report errors or throw
exceptions when failures are detected.*

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
