// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

// Ensure the tests defined in this file always emulate a client compiled for Debug
#define DEBUG

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Verify that the message propagates to the trace listeners if
/// the test project compiles with DEBUG (and the supplied condition is as appropriate).
/// </summary>
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name must match first type name", Justification = "By design")]
public class ReportDebugTests : IDisposable
{
    private const string FailureMessage = "failure";
    private const string DefaultFailureMessage = "A recoverable error has been detected.";

    private AssertDialogSuppression suppressAssertUi = new AssertDialogSuppression();

    public void Dispose()
    {
        this.suppressAssertUi.Dispose();
    }

    [Test]
    public void If()
    {
        using (var listener = new RecordingTraceListener())
        {
            Report.If(false, FailureMessage);
            Report.If(true, FailureMessage);
            listener.AssertReported(FailureMessage);
        }
    }

    [Test]
    public void IfNot()
    {
        using (var listener = new RecordingTraceListener())
        {
            Report.IfNot(true, FailureMessage);
            Report.IfNot(false, FailureMessage);
            listener.AssertReported(FailureMessage);
        }
    }

    [Test]
    public void IfNot_Format1Arg()
    {
        using (var listener = new RecordingTraceListener())
        {
            Report.IfNot(true, "a{0}c", "b");
            Report.IfNot(false, "a{0}c", "b");
            listener.AssertReported("abc");
        }
    }

    [Test]
    public void IfNot_Format2Arg()
    {
        using (var listener = new RecordingTraceListener())
        {
            Report.IfNot(true, "a{0}{1}d", "b", "c");
            Report.IfNot(false, "a{0}{1}d", "b", "c");
            listener.AssertReported("abcd");
        }
    }

    [Test]
    public void IfNot_FormatNArg()
    {
        using (var listener = new RecordingTraceListener())
        {
            Report.IfNot(true, "a{0}{1}{2}e", "b", "c", "d");
            Report.IfNot(false, "a{0}{1}{2}e", "b", "c", "d");
            listener.AssertReported("abcde");
        }
    }

    [Test]
    public void IfNot_InterpolatedString()
    {
        int formatCount = 0;
        string FormattingMethod()
        {
            formatCount++;
            return "b";
        }

        using (var listener = new RecordingTraceListener())
        {
            Report.IfNot(true, $"a{FormattingMethod()}c");
            Assert.Equal(0, formatCount);
            Report.IfNot(false, $"a{FormattingMethod()}c");
            Assert.Equal(1, formatCount);
            listener.AssertReported("abc");
        }
    }

    [Test]
    public void IfNotPresent()
    {
        using (var listener = new RecordingTraceListener())
        {
            string? possiblyPresent = "not missing";
            string missingTypeName = possiblyPresent.GetType().FullName!;
            Report.IfNotPresent(possiblyPresent);
            possiblyPresent = null;
            Report.IfNotPresent(possiblyPresent);
            listener.AssertReported(message => message?.Contains(missingTypeName) == true);
        }
    }

    [Test]
    public void Fail()
    {
        using (var listener = new RecordingTraceListener())
        {
            Report.Fail(FailureMessage);
            listener.AssertReported(FailureMessage);
        }
    }

    [Test]
    public void Fail_DefaultMessage()
    {
        using (var listener = new RecordingTraceListener())
        {
            Report.Fail();
            listener.AssertReported(DefaultFailureMessage);
        }
    }
}
