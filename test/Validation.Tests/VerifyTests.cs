// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

public class VerifyTests
{
    [Fact]
    public void Operation()
    {
        Verify.Operation(true, "Should not throw");
        Verify.Operation(true, "Should not throw", "arg1");
        Verify.Operation(true, "Should not throw", "arg1", "arg2");
        Verify.Operation(true, "Should not throw", "arg1", "arg2", "arg3");

        Assert.Throws<InvalidOperationException>(() => Verify.Operation(false, "throw"));
        Assert.Throws<InvalidOperationException>(() => Verify.Operation(false, "throw", "arg1"));
        Assert.Throws<InvalidOperationException>(() => Verify.Operation(false, "throw", "arg1", "arg2"));
        Assert.Throws<InvalidOperationException>(() => Verify.Operation(false, "throw", "arg1", "arg2", "arg3"));
    }

    [Fact]
    public void Operation_InterpolatedString()
    {
        int formatCount = 0;
        string FormattingMethod()
        {
            formatCount++;
            return "generated string";
        }

        Verify.Operation(true, $"Some {FormattingMethod()} method.");
        Assert.Equal(0, formatCount);

        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => Verify.Operation(false, $"Some {FormattingMethod()} method."));
        Assert.Equal(1, formatCount);
        Assert.StartsWith("Some generated string method.", ex.Message);
    }

    [Fact]
    public void Operation_ResourceManager()
    {
        AssertThrows(TestStrings.GetResourceString(TestStrings.SomeError), c => Verify.Operation(c, TestStrings.ResourceManager, TestStrings.SomeError));
        AssertThrows(TestStrings.FormatSomeError1Arg("A"), c => Verify.Operation(c, TestStrings.ResourceManager, TestStrings.SomeError1Arg, "A"));
        AssertThrows(TestStrings.FormatSomeError2Args("A", "B"), c => Verify.Operation(c, TestStrings.ResourceManager, TestStrings.SomeError2Args, "A", "B"));
        AssertThrows(TestStrings.FormatSomeError3Args("A", "B", "C"), c => Verify.Operation(c, TestStrings.ResourceManager, TestStrings.SomeError3Args, "A", "B", "C"));

        static void AssertThrows(string? expectedMessage, Action<bool> action)
        {
            action(true);

            InvalidOperationException actual = Assert.Throws<InvalidOperationException>(() => action(false));
            Assert.Equal(expectedMessage, actual.Message);
        }
    }

    [Fact]
    public void OperationWithHelp()
    {
        Verify.OperationWithHelp(true, "message", "helpLink");
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => Verify.OperationWithHelp(false, "message", "helpLink"));
        Assert.Equal("message", ex.Message);
        Assert.Equal("helpLink", ex.HelpLink);
    }

    [Fact]
    public void NotDisposed()
    {
        Verify.NotDisposed(true, "message");
        ObjectDisposedException actualException = Assert.Throws<ObjectDisposedException>(() => Verify.NotDisposed(false, "message"));
        Assert.Equal(string.Empty, actualException.ObjectName);
        Assert.Equal("message", actualException.Message);

        Assert.Throws<ObjectDisposedException>(() => Verify.NotDisposed(false, null));
        Assert.Throws<ObjectDisposedException>(() => Verify.NotDisposed(false, (object?)null));

        actualException = Assert.Throws<ObjectDisposedException>(() => Verify.NotDisposed(false, "hi", "message"));
        string expectedObjectName = typeof(string).FullName!;
        Assert.Equal(expectedObjectName, actualException.ObjectName);
        Assert.Equal(new ObjectDisposedException(expectedObjectName, "message").Message, actualException.Message);

        actualException = Assert.Throws<ObjectDisposedException>(() => Verify.NotDisposed(false, new object()));
        Assert.Equal(typeof(object).FullName, actualException.ObjectName);
    }

    [Fact]
    public void NotDisposed_Observable()
    {
        var observable = new Disposable();
        Verify.NotDisposed(observable);
        observable.Dispose();
        Assert.Throws<ObjectDisposedException>(() => Verify.NotDisposed(observable));
        Assert.Throws<ObjectDisposedException>(() => Verify.NotDisposed(observable, "message"));
    }

    [Fact]
    public void FailOperation()
    {
        Assert.Throws<InvalidOperationException>(() => Verify.FailOperation("message", "arg1"));
    }

    [Fact]
    public void HResult()
    {
        const int E_INALIDARG = unchecked((int)0x80070057);
        const int E_FAIL = unchecked((int)0x80004005);
        Verify.HResult(0);
        Assert.Throws<ArgumentException>(() => Verify.HResult(E_INALIDARG));
        Assert.Throws<COMException>(() => Verify.HResult(E_FAIL));
        Assert.Throws<ArgumentException>(() => Verify.HResult(E_INALIDARG, ignorePreviousComCalls: true));
        Assert.Throws<COMException>(() => Verify.HResult(E_FAIL, ignorePreviousComCalls: true));
    }

    private class Disposable : IDisposableObservable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            this.IsDisposed = true;
        }
    }
}
