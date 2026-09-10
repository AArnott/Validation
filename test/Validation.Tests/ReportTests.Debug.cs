// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

// Ensure the tests defined in this file always emulate a client compiled for Debug
#define DEBUG

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Moq;

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

    [Fact]
    public void If()
    {
        using (DisposableValue<Mock<TraceListener>> listener = Listen())
        {
            Report.If(false, FailureMessage);
            listener.Value.Setup(l => l.WriteLine(FailureMessage)).Verifiable();
            SetupFail(listener.Value, FailureMessage);
            Report.If(true, FailureMessage);
        }
    }

    [Fact]
    public void IfNot()
    {
        using (DisposableValue<Mock<TraceListener>> listener = Listen())
        {
            Report.IfNot(true, FailureMessage);
            listener.Value.Setup(l => l.WriteLine(FailureMessage)).Verifiable();
            SetupFail(listener.Value, FailureMessage);
            Report.IfNot(false, FailureMessage);
        }
    }

    [Fact]
    public void IfNot_Format1Arg()
    {
        using (DisposableValue<Mock<TraceListener>> listener = Listen())
        {
            Report.IfNot(true, "a{0}c", "b");
            listener.Value.Setup(l => l.WriteLine("abc")).Verifiable();
            SetupFail(listener.Value, "abc");
            Report.IfNot(false, "a{0}c", "b");
        }
    }

    [Fact]
    public void IfNot_Format2Arg()
    {
        using (DisposableValue<Mock<TraceListener>> listener = Listen())
        {
            Report.IfNot(true, "a{0}{1}d", "b", "c");
            listener.Value.Setup(l => l.WriteLine("abcd")).Verifiable();
            SetupFail(listener.Value, "abcd");
            Report.IfNot(false, "a{0}{1}d", "b", "c");
        }
    }

    [Fact]
    public void IfNot_FormatNArg()
    {
        using (DisposableValue<Mock<TraceListener>> listener = Listen())
        {
            Report.IfNot(true, "a{0}{1}{2}e", "b", "c", "d");
            listener.Value.Setup(l => l.WriteLine("abcde")).Verifiable();
            SetupFail(listener.Value, "abcde");
            Report.IfNot(false, "a{0}{1}{2}e", "b", "c", "d");
        }
    }

    [Fact]
    public void IfNot_InterpolatedString()
    {
        int formatCount = 0;
        string FormattingMethod()
        {
            formatCount++;
            return "b";
        }

        using (DisposableValue<Mock<TraceListener>> listener = Listen())
        {
            Report.IfNot(true, $"a{FormattingMethod()}c");
            Assert.Equal(0, formatCount);
            listener.Value.Setup(l => l.WriteLine("abc")).Verifiable();
            SetupFail(listener.Value, "abc");
            Report.IfNot(false, $"a{FormattingMethod()}c");
            Assert.Equal(1, formatCount);
        }
    }

    [Fact]
    public void IfNotPresent()
    {
        using (DisposableValue<Mock<TraceListener>> listener = Listen())
        {
            string? possiblyPresent = "not missing";
            string missingTypeName = possiblyPresent.GetType().FullName!;
            Report.IfNotPresent(possiblyPresent);
            listener.Value.Setup(l => l.WriteLine(It.Is<string>(v => v.Contains(missingTypeName)))).Verifiable();
            SetupFail(listener.Value, v => v.Contains(missingTypeName));
            possiblyPresent = null;
            Report.IfNotPresent(possiblyPresent);
        }
    }

    [Fact]
    public void Fail()
    {
        using (DisposableValue<Mock<TraceListener>> listener = Listen())
        {
            listener.Value.Setup(l => l.WriteLine(FailureMessage)).Verifiable();
            SetupFail(listener.Value, FailureMessage);
            Report.Fail(FailureMessage);
        }
    }

    [Fact]
    public void Fail_DefaultMessage()
    {
        using (DisposableValue<Mock<TraceListener>> listener = Listen())
        {
            listener.Value.Setup(l => l.WriteLine(DefaultFailureMessage)).Verifiable();
            SetupFail(listener.Value, DefaultFailureMessage);
            Report.Fail();
        }
    }

    private static void SetupFail(Mock<TraceListener> listener, string message)
    {
        listener.Setup(l => l.Fail(message, It.IsAny<string>())).Verifiable();
    }

    private static void SetupFail(Mock<TraceListener> listener, System.Linq.Expressions.Expression<Func<string, bool>> match)
    {
        listener.Setup(l => l.Fail(It.Is(match), It.IsAny<string>())).Verifiable();
    }

    private static DisposableValue<Mock<TraceListener>> Listen()
    {
        var mockListener = new Mock<TraceListener>(MockBehavior.Strict);
        Trace.Listeners.Add(mockListener.Object);
        return new DisposableValue<Mock<TraceListener>>(
            mockListener,
            () =>
            {
                Trace.Listeners.Remove(mockListener.Object);
                mockListener.Verify();
            });
    }
}
