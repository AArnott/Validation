# Copilot instructions for this repository

## High level guidance

* Review the `CONTRIBUTING.md` file for instructions to build and test the software.
* Run the `.github/Prime-ForCopilot.ps1` script before running any `dotnet` or `msbuild` commands.
  If you see build errors about missing git objects or a shallow clone, run this script again.

## Software Design

* Design APIs to be highly testable, and test all functionality.
* Avoid introducing binary breaking changes in public APIs of projects under `src` unless their project files have `IsPackable` set to `false`.

## Coding style

* Honor StyleCop rules and fix any reported build warnings *after* getting tests to pass.
* In C# files, use namespace *statements* instead of namespace *blocks* for all new files.
* Add API doc comments to all new public and internal members in shipping code under `src`. Tests and samples do not require XML API documentation; do not add XML docs to test or sample members solely to satisfy this rule.
