﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE file in the project root for full license information.

using System;

namespace Validation;

/// <summary>
/// A disposable object that also provides a safe way to query its disposed status.
/// </summary>
public interface IDisposableObservable : IDisposable
{
    /// <summary>
    /// Gets a value indicating whether this instance has been disposed.
    /// </summary>
    /// <value><see langword="true"/> if this instance has been disposed.</value>
    bool IsDisposed { get; }
}
