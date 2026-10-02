// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Diagnostics;

/// <summary>Records trace output and assertion failures for Report tests.</summary>
internal sealed class RecordingTraceListener : TraceListener
{
    private readonly List<string?> writes = new();
    private readonly List<(string? Message, string? Detail)> failures = new();

    /// <summary>Initializes a new instance of the <see cref="RecordingTraceListener"/> class and registers it to capture trace events.</summary>
    public RecordingTraceListener()
    {
        Trace.Listeners.Add(this);
    }

    /// <inheritdoc/>
    public override void Write(string? message)
    {
        this.writes.Add(message);
    }

    /// <inheritdoc/>
    public override void WriteLine(string? message)
    {
        this.writes.Add(message);
    }

    /// <inheritdoc/>
    public override void Fail(string? message)
    {
        this.failures.Add((message, null));
    }

    /// <inheritdoc/>
    public override void Fail(string? message, string? detailMessage)
    {
        this.failures.Add((message, detailMessage));
    }

    /// <summary>Checks that exactly one message and assertion failure were reported.</summary>
    internal void AssertReported(string expectedMessage)
    {
        this.AssertReported(message => message == expectedMessage);
    }

    /// <summary>Checks that exactly one matching message and assertion failure were reported.</summary>
    internal void AssertReported(Func<string?, bool> matches)
    {
        Assert.Single(this.writes);
        Assert.True(matches(this.writes[0]));
        Assert.Single(this.failures);
        Assert.True(matches(this.failures[0].Message));
#if NET
        Assert.Equal(string.Empty, this.failures[0].Detail);
#endif
    }

    /// <summary>Checks that no trace messages or assertion failures were reported.</summary>
    internal void AssertNoReports()
    {
        Assert.Empty(this.writes);
        Assert.Empty(this.failures);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Trace.Listeners.Remove(this);
        }

        base.Dispose(disposing);
    }
}
