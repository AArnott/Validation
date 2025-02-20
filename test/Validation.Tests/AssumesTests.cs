﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System.Runtime.Serialization.Formatters.Binary;

public partial class AssumesTests : IDisposable
{
    private const string TestMessage = "Some test message.";
    private AssertDialogSuppression suppressAssertUi = new AssertDialogSuppression();

    public void Dispose()
    {
        this.suppressAssertUi.Dispose();
    }

    [Fact]
    public void True()
    {
        Assumes.True(true);
        Assert.ThrowsAny<Exception>(() => Assumes.True(false, TestMessage));
        Assert.ThrowsAny<Exception>(() => Assumes.True(false, TestMessage, "arg1"));
        Assert.ThrowsAny<Exception>(() => Assumes.True(false, TestMessage, "arg1", "arg2"));
    }

    [Fact]
    public void False()
    {
        Assumes.False(false);
        Assert.ThrowsAny<Exception>(() => Assumes.False(true, TestMessage));
        Assert.ThrowsAny<Exception>(() => Assumes.False(true, TestMessage, "arg1"));
        Assert.ThrowsAny<Exception>(() => Assumes.False(true, TestMessage, "arg1", "arg2"));
    }

    [Fact]
    public void True_InterpolatedString()
    {
        int formatCount = 0;
        string FormattingMethod()
        {
            formatCount++;
            return "generated string";
        }

        Assumes.True(true, $"Some {FormattingMethod()} method.");
        Assert.Equal(0, formatCount);

        Exception ex = Assert.ThrowsAny<Exception>(() => Assumes.True(false, $"Some {FormattingMethod()} method."));
        Assert.Equal(1, formatCount);
        Assert.StartsWith("Some generated string method.", ex.Message);
    }

    [Fact]
    public void False_InterpolatedString()
    {
        int formatCount = 0;
        string FormattingMethod()
        {
            formatCount++;
            return "generated string";
        }

        Assumes.False(false, $"Some {FormattingMethod()} method.");
        Assert.Equal(0, formatCount);

        Exception ex = Assert.ThrowsAny<Exception>(() => Assumes.False(true, $"Some {FormattingMethod()} method."));
        Assert.Equal(1, formatCount);
        Assert.StartsWith("Some generated string method.", ex.Message);
    }

    [Fact]
    public void Fail()
    {
        Assert.ThrowsAny<Exception>(() => Assumes.Fail("some message", new InvalidOperationException()));
    }

    [Fact]
    public void NotNull()
    {
        Assert.ThrowsAny<Exception>(() => Assumes.NotNull((object?)null));
        Assumes.NotNull("success");
    }

    [Fact]
    public void NotNull_NullableStruct()
    {
        Assert.ThrowsAny<Exception>(() => Assumes.NotNull((int?)null));
        Assumes.NotNull((int?)5);
    }

    [Fact]
    public void Null()
    {
        Assert.ThrowsAny<Exception>(() => Assumes.Null("not null"));
        Assumes.Null((object?)null);
    }

    [Fact]
    public void Null_NullableStruct()
    {
        Assert.ThrowsAny<Exception>(() => Assumes.Null((int?)5));
        Assumes.Null((int?)null);
    }

    [Fact]
    public void NotNullOrEmpty()
    {
        ICollection<string> collection = new string[] { "foo" };

        Assert.ThrowsAny<Exception>(() => Assumes.NotNullOrEmpty(null));
        Assert.ThrowsAny<Exception>(() => Assumes.NotNullOrEmpty(string.Empty));
        Assumes.NotNullOrEmpty("success");

        Assert.ThrowsAny<Exception>(() => Assumes.NotNullOrEmpty((ICollection<string>?)null));
        Assert.ThrowsAny<Exception>(() => Assumes.NotNullOrEmpty(collection.Take(0).ToList()));
        Assumes.NotNullOrEmpty(collection);

        Assert.ThrowsAny<Exception>(() => Assumes.NotNullOrEmpty((IEnumerable<string>?)null));
        Assert.ThrowsAny<Exception>(() => Assumes.NotNullOrEmpty(collection.Take(0)));
        Assumes.NotNullOrEmpty(collection.Take(1));
    }

    [Fact]
    public void Is()
    {
        Assert.ThrowsAny<Exception>(() => Assumes.Is<string>(null));
        Assert.ThrowsAny<Exception>(() => Assumes.Is<string>(45));
        Assert.ThrowsAny<Exception>(() => Assumes.Is<string>(new object()));
        Assumes.Is<string>("hi");
    }

    [Fact]
    public void NotReachable()
    {
        Assert.ThrowsAny<Exception>(Assumes.NotReachable);
    }

    [Fact]
    public void NotReachableOfT()
    {
        Assert.ThrowsAny<Exception>(() => Assumes.NotReachable<int>());
        Assert.ThrowsAny<Exception>(() => Assumes.NotReachable<object>());
    }

    [Fact]
    public void Present()
    {
        IServiceProvider? someService = null;
        Assert.ThrowsAny<Exception>(() => Assumes.Present(someService));
        Assumes.Present("hi");
    }

#if !NET
    [Fact]
    public void InternalErrorException_IsSerializable()
    {
        try
        {
            Assumes.False(true);
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
        {
#pragma warning disable SYSLIB0011 // Type or member is obsolete
            var formatter = new BinaryFormatter();
            using var ms = new MemoryStream();
            formatter.Serialize(ms, ex);
            ms.Position = 0;
            var ex2 = (Exception)formatter.Deserialize(ms);
            Assert.IsType(ex.GetType(), ex2);
            Assert.Equal(ex.Message, ex2.Message);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
        }
    }
#endif
}
