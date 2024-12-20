# Features

* Simple, succinct argument validation with the @Validation.Requires class.
* Verify appropriate time and place for calls with the @Validation.Verify class.
* Internal integrity validation with the @Validation.Assumes class.
* Assert assumptions only in debug builds with the @Validation.Report class.

## High performance

* Highly-tuned success paths minimize the perf impact of calling these APIs.
* Absolutely no allocations in success paths on .NET 9, and very few (if any) on older runtimes.
