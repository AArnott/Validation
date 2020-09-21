// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

// Ensure the tests defined in this file always emulate a client compiled for Debug
#define DEBUG

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Validation;

using Xunit;

/// <summary>
/// Verify that the message propagates to the trace listeners if
/// the test project compiles with DEBUG (and the supplied condition is as appropriate).
/// </summary>
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name must match first type name", Justification = "By design")]
public class ReportDebugTests
{
    private const string FailureMessage = "failure";
    private const string DefaultFailureMessage = "A recoverable error has been detected.";

    public ReportDebugTests()
    {
        Trace.Listeners.Clear();
        Trace.Listeners.Add(new TestingTraceListener());
    }

    private void AssertDoesNotThrow(Action action)
    {
        try
        {
            action();
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception e)
        {
            Assert.False(false, e.Message);
        }
#pragma warning restore CA1031 // Do not catch general exception types

        Assert.True(true);
    }

    [Fact]
    public void If()
    {
        AssertDoesNotThrow(() => Report.If(false, FailureMessage));
        Assert.Equal(FailureMessage, Assert.ThrowsAny<Exception>(() => Report.If(true, FailureMessage)).Message);
    }

    [Fact]
    public void IfNot()
    {
        AssertDoesNotThrow(() => Report.IfNot(true, FailureMessage));
        Assert.Equal(FailureMessage, Assert.ThrowsAny<Exception>(() => Report.IfNot(false, FailureMessage)).Message);
    }

    [Fact]
    public void IfNot_Format1Arg()
    {
        AssertDoesNotThrow(() => Report.IfNot(true, "a{0}c", "b"));
        Assert.Equal("abc", Assert.ThrowsAny<Exception>(() => Report.IfNot(false, "a{0}c", "b")).Message);
    }

    [Fact]
    public void IfNot_Format2Arg()
    {
        AssertDoesNotThrow(() => Report.IfNot(true, "a{0}{1}d", "b", "c"));
        Assert.Equal("abcd", Assert.ThrowsAny<Exception>(() => Report.IfNot(false, "a{0}{1}d", "b", "c")).Message);
    }

    [Fact]
    public void IfNot_FormatNArg()
    {
        AssertDoesNotThrow(() => Report.IfNot(true, "a{0}{1}{2}e", "b", "c", "d"));
        Assert.Equal("abcde", Assert.ThrowsAny<Exception>(() => Report.IfNot(false, "a{0}{1}{2}e", "b", "c", "d")).Message);
    }

    [Fact]
    public void IfNotPresent()
    {
        string? possiblyPresent = "not missing";
        string missingTypeName = possiblyPresent.GetType().FullName!;
        AssertDoesNotThrow(() => Report.IfNotPresent(possiblyPresent));

        possiblyPresent = null;
        Assert.Contains(missingTypeName, Assert.ThrowsAny<Exception>(() => Report.IfNotPresent(possiblyPresent)).Message);
    }

    [Fact]
    public void Fail()
    {
        Assert.Equal(FailureMessage, Assert.ThrowsAny<Exception>(() => Report.Fail(FailureMessage)).Message);
    }

    [Fact]
    public void Fail_DefaultMessage()
    {
        Assert.Equal(DefaultFailureMessage, Assert.ThrowsAny<Exception>(() => Report.Fail()).Message);
    }
}
