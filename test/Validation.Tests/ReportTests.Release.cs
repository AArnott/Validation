// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

// Ensure the tests defined in this file always emulate a client compiled for Release
#undef DEBUG

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Validation;

using Xunit;

/// <summary>
/// Verify that the message does NOT propagate to the trace listeners when
/// the test project compiles without DEBUG.
/// </summary>
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name must match first type name", Justification = "By design")]
public class ReportReleaseTests
{
    private const string FailureMessage = "failure";
    private const string DefaultFailureMessage = "A recoverable error has been detected.";

    public ReportReleaseTests()
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
        catch (Exception e)
        {
            Assert.False(false, e.Message);
        }

        Assert.True(true);
    }

    [Fact]
    public void If()
    {
        AssertDoesNotThrow(() => Report.If(false, FailureMessage));
        AssertDoesNotThrow(() => Report.If(true, FailureMessage));
    }

    [Fact]
    public void IfNot()
    {
        AssertDoesNotThrow(() => Report.IfNot(true, FailureMessage));
        AssertDoesNotThrow(() => Report.IfNot(false, FailureMessage));
    }

    [Fact]
    public void IfNot_Format1Arg()
    {
        AssertDoesNotThrow(() => Report.IfNot(true, "a{0}c", "b"));
        AssertDoesNotThrow(() => Report.IfNot(false, "a{0}c", "b"));
    }

    [Fact]
    public void IfNot_Format2Arg()
    {
        AssertDoesNotThrow(() => Report.IfNot(true, "a{0}{1}d", "b", "c"));
        AssertDoesNotThrow(() => Report.IfNot(false, "a{0}{1}d", "b", "c"));
    }

    [Fact]
    public void IfNot_FormatNArg()
    {
        AssertDoesNotThrow(() => Report.IfNot(true, "a{0}{1}{2}e", "b", "c", "d"));
        AssertDoesNotThrow(() => Report.IfNot(false, "a{0}{1}{2}e", "b", "c", "d"));
    }

    [Fact]
    public void IfNotPresent()
    {
        string? possiblyPresent = "not missing";
        AssertDoesNotThrow(() => Report.IfNotPresent(possiblyPresent));
        possiblyPresent = null;
        AssertDoesNotThrow(() => Report.IfNotPresent(possiblyPresent));
    }

    [Fact]
    public void Fail()
    {
        AssertDoesNotThrow(() => Report.Fail(FailureMessage));
    }

    [Fact]
    public void Fail_DefaultMessage()
    {
        AssertDoesNotThrow(() => Report.Fail());
    }
}
