# Features

* Simple, succinct argument validation with the @Validation.Requires class.
* Verify appropriate time and place for calls with the @Validation.Verify class.
* Internal integrity validation with the @Validation.Assumes class.
* Assert assumptions only in debug builds with the @Validation.Report class.

## High performance

The APIs are highly-tuned success paths minimize the perf impact of calling these APIs.
There are absolutely no allocations in success paths on .NET 9, and very few (if any) on older runtimes.

* Optimized interpolated strings support so you can use `$"The value {value} is too low"` without any allocations unless the assertion fails.
* Optimized ResourceManager access to avoid loading a string resource unless the assertion fails.
* C# 13 `params ReadOnlySpan<object?>` overloads on .NET 9 to avoid allocations in success paths when many formatting arguments are required.
* Several small formatting argument count overloads to avoid array allocations in success paths on older runtimes.
