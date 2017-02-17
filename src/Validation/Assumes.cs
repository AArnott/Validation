// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

namespace Validation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Common runtime checks that throw public error exceptions upon failure.
    /// </summary>
    public static partial class Assumes
    {
        /// <summary>
        /// Throws an exception if the specified value is null.
        /// </summary>
        /// <typeparam name="T">The type of value to test.</typeparam>
        /// <param name="value">The value.</param>
        [DebuggerStepThrough]
        public static void NotNull<T>([ValidatedNotNull]T value)
            where T : class
        {
            True(value != null);
        }

        /// <summary>
        /// Throws an exception if the specified value is null or empty.
        /// </summary>
        /// <param name="value">The value.</param>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty([ValidatedNotNull]string value)
        {
            NotNull(value);
            True(value.Length > 0);
            True(value[0] != '\0');
        }

        /// <summary>
        /// Throws an exception if the specified value is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of value to test.</typeparam>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty<T>([ValidatedNotNull]ICollection<T> values)
        {
            Assumes.NotNull(values);
            Assumes.True(values.Count > 0);
        }

        /// <summary>
        /// Throws an exception if the specified value is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of value to test.</typeparam>
        /// <param name="values">The values.</param>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty<T>([ValidatedNotNull]IEnumerable<T> values)
        {
            Assumes.NotNull(values);
            Assumes.True(values.Any());
        }

        /// <summary>
        /// Throws an exception if the specified value is not null.
        /// </summary>
        /// <typeparam name="T">The type of value to test.</typeparam>
        /// <param name="value">The value.</param>
        [DebuggerStepThrough]
        public static void Null<T>(T value)
            where T : class
        {
            True(value == null);
        }

        /// <summary>
        /// Throws an exception if the specified object is not of a given type.
        /// </summary>
        /// <typeparam name="T">The type the value is expected to be.</typeparam>
        /// <param name="value">The value to test.</param>
        [DebuggerStepThrough]
        public static void Is<T>(object value)
        {
            True(value is T);
        }

        /// <summary>
        /// Throws an public exception if a condition evaluates to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void False(bool condition, string message = null)
        {
            if (condition)
            {
                Fail(message);
            }
        }

        /// <summary>
        /// Throws an public exception if a condition evaluates to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void False(bool condition, string unformattedMessage, object arg1)
        {
            if (condition)
            {
                Fail(Format(unformattedMessage, arg1));
            }
        }

        /// <summary>
        /// Throws an public exception if a condition evaluates to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void False(bool condition, string unformattedMessage, params object[] args)
        {
            if (condition)
            {
                Fail(Format(unformattedMessage, args));
            }
        }

        /// <summary>
        /// Throws an public exception if a condition evaluates to false.
        /// </summary>
        [DebuggerStepThrough]
        public static void True(bool condition, string message = null)
        {
            if (!condition)
            {
                Fail(message);
            }
        }

        /// <summary>
        /// Throws an public exception if a condition evaluates to false.
        /// </summary>
        [DebuggerStepThrough]
        public static void True(bool condition, string unformattedMessage, object arg1)
        {
            if (!condition)
            {
                Fail(Format(unformattedMessage, arg1));
            }
        }

        /// <summary>
        /// Throws an public exception if a condition evaluates to false.
        /// </summary>
        [DebuggerStepThrough]
        public static void True(bool condition, string unformattedMessage, params object[] args)
        {
            if (!condition)
            {
                Fail(Format(unformattedMessage, args));
            }
        }

        /// <summary>
        /// Throws an public exception.
        /// </summary>
        /// <returns>Nothing.  This method always throws.  But the signature allows calling code to "throw" this method for C# syntax reasons.</returns>
        [DebuggerStepThrough]
        public static Exception NotReachable()
        {
            // Keep these two as separate lines of code, so the debugger can come in during the assert dialog
            // that the exception's constructor displays, and the debugger can then be made to skip the throw
            // in order to continue the investigation.
            var exception = new InternalErrorException();
            bool proceed = true; // allows debuggers to skip the throw statement
            if (proceed)
            {
                throw exception;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Verifies that a value is not null, and throws an exception about a missing service otherwise.
        /// </summary>
        /// <typeparam name="T">The interface of the imported part.</typeparam>
        [DebuggerStepThrough]
        public static void Present<T>(T component)
        {
            if (component == null)
            {
#if NET35
                Type coreType = typeof(T);
#else
                Type coreType = PrivateErrorHelpers.TrimGenericWrapper(typeof(T), typeof(Lazy<>));
#endif
                Fail(string.Format(CultureInfo.CurrentCulture, Strings.ServiceMissing, coreType.FullName));
            }
        }

        /// <summary>
        /// Throws an public exception.
        /// </summary>
        /// <returns>Nothing, as this method always throws.  The signature allows for "throwing" Fail so C# knows execution will stop.</returns>
        [DebuggerStepThrough]
        public static Exception Fail(string message = null, bool showAssert = true)
        {
            var exception = new InternalErrorException(message, showAssert);
            bool proceed = true; // allows debuggers to skip the throw statement
            if (proceed)
            {
                throw exception;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Throws an public exception.
        /// </summary>
        /// <returns>Nothing, as this method always throws.  The signature allows for "throwing" Fail so C# knows execution will stop.</returns>
        public static Exception Fail(string message, Exception innerException, bool showAssert = true)
        {
            var exception = new InternalErrorException(message, innerException, showAssert);
            bool proceed = true; // allows debuggers to skip the throw statement
            if (proceed)
            {
                throw exception;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Helper method that formats string arguments.
        /// </summary>
        /// <returns>The formatted strings.</returns>
        private static string Format(string format, params object[] arguments)
        {
            return PrivateErrorHelpers.Format(format, arguments);
        }
    }
}
