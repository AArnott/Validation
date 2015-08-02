//-----------------------------------------------------------------------
// <copyright file="IDisposableObservable.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Validation
{
    using System;

    /// <summary>
    /// A disposable object that also provides a safe way to query its disposed status.
    /// </summary>
    public interface IDisposableObservable : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this instance has been disposed.
        /// </summary>
        /// <value><c>true</c> if this instance has been disposed.</value>
        bool IsDisposed { get; }
    }
}
