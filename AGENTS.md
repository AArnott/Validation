# Copilot instructions for this repository

## High level guidance

* Review the `CONTRIBUTING.md` file for instructions to build and test the software.
* Run the `.github/Prime-ForCopilot.ps1` script before running any `dotnet` or `msbuild` commands.
  If you see build errors about missing git objects or a shallow clone, run this script again.

## Software Design

* Design APIs to be highly testable, and test all functionality.
* Avoid introducing binary breaking changes in public APIs of projects under `src` unless their project files have `IsPackable` set to `false`.

## Testing

This repository uses TUnit with Microsoft.Testing.Platform (MTP v2), while retaining xUnit assertions. Traditional VSTest `--filter` syntax does not work.

* Test projects are under `test` and are named after their corresponding shipping projects with a `.Tests` suffix.
* Pass TUnit filters after `--` using `--treenode-filter`.
* Wildcards `*` are supported in tree-node segments.

### Running Tests

**Run all tests**:
```bash
dotnet test --no-build -c Release
```

**Run the Validation tests**:
```bash
dotnet test --project test/Validation.Tests/Validation.Tests.csproj --no-build -c Release
```

**Run a single test method**:
```bash
dotnet test --project test/Validation.Tests/Validation.Tests.csproj --no-build -c Release -- --treenode-filter "/*/*/ClassName/MethodName"
```

**Run all tests in a class**:
```bash
dotnet test --project test/Validation.Tests/Validation.Tests.csproj --no-build -c Release -- --treenode-filter "/*/*/ClassName/*"
```

**Run tests matching a method wildcard**:
```bash
dotnet test --project test/Validation.Tests/Validation.Tests.csproj --no-build -c Release -- --treenode-filter "/*/*/*/*Pattern*"
```

**Run tests with a specific property**:
```bash
dotnet test --project test/Validation.Tests/Validation.Tests.csproj --no-build -c Release -- --treenode-filter "/*/*/*/*[PropertyName=value]"
```

**Run tests for a specific framework**:
```bash
dotnet test --project test/Validation.Tests/Validation.Tests.csproj --no-build -c Release --framework net8.0
```

**List available tests**:
```bash
cd test/Validation.Tests
dotnet run --no-build -c Release --framework net8.0 -- --list-tests
```

## Coding style

* Honor StyleCop rules and fix reported build warnings after getting tests to pass.
* In C# files, use namespace statements instead of namespace blocks for all new files.
* Add API doc comments to all new public and internal members.
