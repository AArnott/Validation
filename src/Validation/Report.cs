// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

namespace Validation
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    /// Common runtime checks that trace messages and invoke an assertion failure,
    /// but does *not* throw exceptions.
    /// </summary>
    public static class Report
    {
        /// <summary>
        /// Reports a certain failure.
        /// </summary>
        [Conditional("DEBUG")]
        public static void Fail([Localizable(false)] string? message = null)
        {
            if (message is null)
            {
                message = "A recoverable error has been detected.";
            }

            Debug.WriteLine(message);
            Debug.Assert(false, message);
        }

        /// <summary>
        /// Reports a certain failure.
        /// </summary>
        [Conditional("DEBUG")]
        public static void Fail([Localizable(false)] string message, params object?[] args)
        {
            Fail(PrivateErrorHelpers.Format(message, args));
        }

        /// <summary>
        /// Reports an error if a condition evaluates to true.
        /// </summary>
        [Conditional("DEBUG")]
        public static void If(bool condition, [Localizable(false)] string? message = null)
        {
            if (condition)
            {
                Fail(message);
            }
        }

        /// <summary>
        /// Reports an error if a condition does not evaluate to true.
        /// </summary>
        [Conditional("DEBUG")]
        public static void IfNot(bool condition, [Localizable(false)] string? message = null)
        {
            if (!condition)
            {
                Fail(message);
            }
        }

        /// <summary>
        /// Reports an error if a condition does not evaluate to true.
        /// </summary>
        [Conditional("DEBUG")]
        public static void IfNot(bool condition, [Localizable(false)] string message, object? arg1)
        {
            if (!condition)
            {
                Fail(PrivateErrorHelpers.Format(message, arg1));
            }
        }

        /// <summary>
        /// Reports an error if a condition does not evaluate to true.
        /// </summary>
        [Conditional("DEBUG")]
        public static void IfNot(bool condition, [Localizable(false)] string message, object? arg1, object? arg2)
        {
            if (!condition)
            {
                Fail(PrivateErrorHelpers.Format(message, arg1, arg2));
            }
        }

        /// <summary>
        /// Reports an error if a condition does not evaluate to true.
        /// </summary>
        [Conditional("DEBUG")]
        public static void IfNot(bool condition, [Localizable(false)] string message, params object?[] args)
        {
            if (!condition)
            {
                Fail(PrivateErrorHelpers.Format(message, args));
            }
        }

        /// <summary>
        /// Verifies that a value is not null, and reports an error about a missing MEF component otherwise.
        /// </summary>
        /// <typeparam name="T">The interface of the imported part.</typeparam>
        [Conditional("DEBUG")]
        public static void IfNotPresent<T>(T part)
        {
            if (part is null)
            {
#if NET20
                Type coreType = typeof(T);
#else
                Type coreType = PrivateErrorHelpers.TrimGenericWrapper(typeof(T), typeof(Lazy<>));
#endif
                Fail(Strings.ServiceMissing, coreType.FullName);
            }
        }
    }
}