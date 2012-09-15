Validation
==========

*Method input validation and runtime checks that report errors or throw
exceptions when failures are detected.*

This project is available as the [Validation][1] NuGet package.

Basic input validation via the `Requires` class throws an ArgumentException.

	Requires.NotNull(arg1, "arg1");
	Requires.NotNullOrEmpty(arg2, "arg2");

State validation via the `Verify` class throws an InvalidOperationException.

	Verify.Operation(condition, "some error occurred.");

Internal integrity checks via the `Assumes` class throws an
InternalErrorException.

	Assumes.True(condition, "some error");

Warning signs that should not throw exceptions via the `Report` class.

	Report.IfNot(condition, "some error");

[1]: http://nuget.org/packages/Validation "Validation NuGet package"