﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

namespace Validation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Common runtime checks that throw exceptions upon failure.
    /// </summary>
    public static class Verify
    {
        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if a condition is false.
        /// </summary>
        [DebuggerStepThrough]
        public static void Operation(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if a condition is false.
        /// </summary>
        [DebuggerStepThrough]
        public static void Operation(bool condition, string unformattedMessage, object arg1)
        {
            if (!condition)
            {
                throw new InvalidOperationException(PrivateErrorHelpers.Format(unformattedMessage, arg1));
            }
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if a condition is false.
        /// </summary>
        [DebuggerStepThrough]
        public static void Operation(bool condition, string unformattedMessage, object arg1, object arg2)
        {
            if (!condition)
            {
                throw new InvalidOperationException(PrivateErrorHelpers.Format(unformattedMessage, arg1, arg2));
            }
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if a condition is false.
        /// </summary>
        [DebuggerStepThrough]
        public static void Operation(bool condition, string unformattedMessage, params object[] args)
        {
            if (!condition)
            {
                throw new InvalidOperationException(PrivateErrorHelpers.Format(unformattedMessage, args));
            }
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <returns>
        /// Nothing.  This method always throws.
        /// The signature claims to return an exception to allow callers to throw this method
        /// to satisfy C# execution path constraints.
        /// </returns>
        [DebuggerStepThrough]
        public static Exception FailOperation(string message, params object[] args)
        {
            throw new InvalidOperationException(PrivateErrorHelpers.Format(message, args));
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> if an object is disposed.
        /// </summary>
        [DebuggerStepThrough]
        public static void NotDisposed(IDisposableObservable disposedValue, string message = null)
        {
            Requires.NotNull(disposedValue, "disposedValue");

            if (disposedValue.IsDisposed)
            {
                string objectName = disposedValue != null ? disposedValue.GetType().FullName : string.Empty;
                if (message != null)
                {
                    throw new ObjectDisposedException(objectName, message);
                }
                else
                {
                    throw new ObjectDisposedException(objectName);
                }
            }
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> if a condition is false.
        /// </summary>
        [DebuggerStepThrough]
        public static void NotDisposed(bool condition, object disposedValue, string message = null)
        {
            if (!condition)
            {
                string objectName = disposedValue != null ? disposedValue.GetType().FullName : string.Empty;
                if (message != null)
                {
                    throw new ObjectDisposedException(objectName, message);
                }
                else
                {
                    throw new ObjectDisposedException(objectName);
                }
            }
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> if a condition is false.
        /// </summary>
        [DebuggerStepThrough]
        public static void NotDisposed(bool condition, string message)
        {
            if (!condition)
            {
                throw new ObjectDisposedException(message);
            }
        }
    }
}
