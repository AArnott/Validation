// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

namespace Validation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Common runtime checks that trace messages and invoke an assertion failure,
    /// but does *not* throw exceptions.
    /// </summary>
    public static class Report
    {
        /// <summary>
        /// Verifies that a value is not null, and reports an error about a missing MEF component otherwise.
        /// </summary>
        /// <typeparam name="T">The interface of the imported part.</typeparam>
        [DebuggerStepThrough]
        public static void IfNotPresent<T>(T part)
        {
            if (part == null)
            {
#if NET20
                Type coreType = typeof(T);
#else
                Type coreType = PrivateErrorHelpers.TrimGenericWrapper(typeof(T), typeof(Lazy<>));
#endif
                Fail(Strings.ServiceMissing, coreType.FullName);
            }
        }

        /// <summary>
        /// Reports an error if a condition evaluates to true.
        /// </summary>
        /// <param name="condition">if set to <c>true</c>, an error is reported.</param>
        /// <param name="message">The formatted message.</param>
        [DebuggerStepThrough]
        public static void If(bool condition, string message = null)
        {
            if (condition)
            {
                Fail(message);
            }
        }

        /// <summary>
        /// Reports an error if a condition does not evaluate to true.
        /// </summary>
        /// <param name="condition">if set to <c>false</c>, an error is reported.</param>
        /// <param name="message">The formatted message.</param>
        [DebuggerStepThrough]
        public static void IfNot(bool condition, string message = null)
        {
            if (!condition)
            {
                Fail(message);
            }
        }

        /// <summary>
        /// Reports an error if a condition does not evaluate to true.
        /// </summary>
        /// <param name="condition">if set to <c>false</c>, an error is reported.</param>
        /// <param name="message">The unformatted message.</param>
        /// <param name="arg1">The only formatting argument.</param>
        [DebuggerStepThrough]
        public static void IfNot(bool condition, string message, object arg1)
        {
            if (!condition)
            {
                Fail(PrivateErrorHelpers.Format(message, arg1));
            }
        }

        /// <summary>
        /// Reports an error if a condition does not evaluate to true.
        /// </summary>
        /// <param name="condition">if set to <c>false</c>, an error is reported.</param>
        /// <param name="message">The unformatted message.</param>
        /// <param name="arg1">The first formatting argument.</param>
        /// <param name="arg2">The second formatting argument.</param>
        [DebuggerStepThrough]
        public static void IfNot(bool condition, string message, object arg1, object arg2)
        {
            if (!condition)
            {
                Fail(PrivateErrorHelpers.Format(message, arg1, arg2));
            }
        }

        /// <summary>
        /// Reports an error if a condition does not evaluate to true.
        /// </summary>
        /// <param name="condition">if set to <c>false</c>, an error is reported.</param>
        /// <param name="message">The unformatted message.</param>
        /// <param name="args">The formatting args.</param>
        [DebuggerStepThrough]
        public static void IfNot(bool condition, string message, params object[] args)
        {
            if (!condition)
            {
                Fail(PrivateErrorHelpers.Format(message, args));
            }
        }

        /// <summary>
        /// Reports a certain failure.
        /// </summary>
        /// <param name="message">The message.</param>
        [DebuggerStepThrough]
        public static void Fail(string message = null)
        {
            if (message == null)
            {
                message = "A recoverable error has been detected.";
            }

            Debug.WriteLine(message);
            Debug.Assert(false, message);
        }

        /// <summary>
        /// Reports a certain failure.
        /// </summary>
        [DebuggerStepThrough]
        public static void Fail(string message, params object[] args)
        {
            Fail(PrivateErrorHelpers.Format(message, args));
        }
    }
}
