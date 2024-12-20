﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

internal class DisposableValue<T> : IDisposable
{
    private Action? disposeAction;

    internal DisposableValue(T value, Action disposeAction)
    {
        this.Value = value;
        this.disposeAction = disposeAction;
    }

    internal T Value { get; }

    public void Dispose()
    {
        this.disposeAction?.Invoke();
        this.disposeAction = null;
    }
}
