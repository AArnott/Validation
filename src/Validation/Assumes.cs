// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

namespace Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Common runtime checks that throw public error exceptions upon failure.
    /// </summary>
    public static partial class Assumes
    {
        /// <summary>
        /// Throws an public exception.
        /// </summary>
        /// <returns>Nothing, as this method always throws.  The signature allows for "throwing" Fail so C# knows execution will stop.</returns>
        [DebuggerStepThrough]
        [DoesNotReturn]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception Fail([Localizable(false)] string? message = null)
        {
            var exception = new InternalErrorException(message);
            bool proceed = true; // allows debuggers to skip the throw statement
            if (proceed)
            {
                throw exception;
            }
            else
            {
#pragma warning disable CS8763
                return new Exception();
#pragma warning restore CS8763
            }
        }

        /// <summary>
        /// Throws an public exception.
        /// </summary>
        /// <returns>Nothing, as this method always throws.  The signature allows for "throwing" Fail so C# knows execution will stop.</returns>
        [DebuggerStepThrough]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Fail(string) instead.")]
        [DoesNotReturn]
        public static Exception Fail([Localizable(false)] string? message = null, bool showAssert = true) => Fail(message);

        /// <summary>
        /// Throws an public exception.
        /// </summary>
        /// <returns>Nothing, as this method always throws.  The signature allows for "throwing" Fail so C# knows execution will stop.</returns>
        [DoesNotReturn]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception Fail([Localizable(false)] string? message, Exception? innerException)
        {
            var exception = new InternalErrorException(message, innerException);
            bool proceed = true; // allows debuggers to skip the throw statement
            if (proceed)
            {
                throw exception;
            }
            else
            {
#pragma warning disable CS8763
                return new Exception();
#pragma warning restore CS8763
            }
        }

        /// <summary>
        /// Throws an public exception.
        /// </summary>
        /// <returns>Nothing, as this method always throws.  The signature allows for "throwing" Fail so C# knows execution will stop.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Fail(string, Exception) instead.")]
        [DoesNotReturn]
        public static Exception Fail([Localizable(false)] string? message, Exception? innerException, bool showAssert = true) => Fail(message, innerException);

        /// <summary>
        /// Throws an public exception if a condition evaluates to true.
        /// </summary>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void False([DoesNotReturnIf(true)] bool condition, [Localizable(false)] string? message = null)
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
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void False([DoesNotReturnIf(true)] bool condition, [Localizable(false)] string unformattedMessage, object? arg1)
        {
            if (condition)
            {
                Fail(PrivateErrorHelpers.Format(unformattedMessage, arg1));
            }
        }

        /// <summary>
        /// Throws an public exception if a condition evaluates to true.
        /// </summary>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void False([DoesNotReturnIf(true)] bool condition, [Localizable(false)] string unformattedMessage, params object?[] args)
        {
            if (condition)
            {
                Fail(PrivateErrorHelpers.Format(unformattedMessage, args));
            }
        }

        /// <summary>
        /// Throws an exception if the specified object is not of a given type.
        /// </summary>
        /// <typeparam name="T">The type the value is expected to be.</typeparam>
        /// <param name="value">The value to test.</param>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Is<T>([NotNull] object? value)
        {
            True(value is T);
        }

        /// <summary>
        /// Throws an exception if the specified value is null.
        /// </summary>
        /// <typeparam name="T">The type of value to test.</typeparam>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>([ValidatedNotNull, NotNull] T? value)
            where T : class
        {
            True(value is object);
        }

        /// <summary>
        /// Throws an exception if the specified value is null.
        /// </summary>
        /// <typeparam name="T">The type of value to test.</typeparam>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>([ValidatedNotNull, NotNull] T? value)
            where T : struct
        {
            True(value.HasValue);
        }

        /// <summary>
        /// Throws an exception if the specified value is null or empty.
        /// </summary>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty([ValidatedNotNull, NotNull] string? value)
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
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty<T>([ValidatedNotNull, NotNull] ICollection<T>? values)
        {
            NotNull(values);
            True(values.Count > 0);
        }

        /// <summary>
        /// Throws an exception if the specified value is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of value to test.</typeparam>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty<T>([ValidatedNotNull, NotNull] IEnumerable<T>? values)
        {
            NotNull(values);
            True(values.Any());
        }

        /// <summary>
        /// Unconditionally throws an <see cref="InternalErrorException"/>.
        /// </summary>
        /// <returns>Nothing. This method always throws. But the signature allows calling code to "throw" this method for C# syntax reasons.</returns>
        [DebuggerStepThrough]
        [DoesNotReturn]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
#pragma warning disable CS8763
                return new Exception();
#pragma warning restore CS8763
            }
        }

        /// <summary>
        /// Unconditionally throws an <see cref="InternalErrorException"/>.
        /// </summary>
        /// <typeparam name="T">The type that the method should be typed to return (although it never returns anything).</typeparam>
        /// <returns>Nothing. This method always throws.</returns>
        [DebuggerStepThrough]
        [DoesNotReturn]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: MaybeNull]
        public static T NotReachable<T>()
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
#pragma warning disable CS8763 // A method marked [DoesNotReturn] should not return.
                return default;
#pragma warning restore CS8763 // A method marked [DoesNotReturn] should not return.
            }
        }

        /// <summary>
        /// Throws an exception if the specified value is not null.
        /// </summary>
        /// <typeparam name="T">The type of value to test.</typeparam>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Null<T>(T? value)
            where T : class
        {
            True(value is null);
        }

        /// <summary>
        /// Throws an exception if the specified value is not null.
        /// </summary>
        /// <typeparam name="T">The type of value to test.</typeparam>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Null<T>(T? value)
            where T : struct
        {
            False(value.HasValue);
        }

        /// <summary>
        /// Verifies that a value is not null, and throws an exception about a missing service otherwise.
        /// </summary>
        /// <typeparam name="T">The interface of the imported part.</typeparam>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Present<T>([NotNull] T component)
        {
            if (component is null)
            {
                Type coreType = PrivateErrorHelpers.TrimGenericWrapper(typeof(T), typeof(Lazy<>));
                Fail(PrivateErrorHelpers.Format(Strings.ServiceMissing, coreType.FullName));
            }
        }

        /// <summary>
        /// Throws an public exception if a condition evaluates to false.
        /// </summary>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void True([DoesNotReturnIf(false)] bool condition, [Localizable(false)] string? message = null)
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
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void True([DoesNotReturnIf(false)] bool condition, [Localizable(false)] string unformattedMessage, object? arg1)
        {
            if (!condition)
            {
                Fail(PrivateErrorHelpers.Format(unformattedMessage, arg1));
            }
        }

        /// <summary>
        /// Throws an public exception if a condition evaluates to false.
        /// </summary>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void True([DoesNotReturnIf(false)] bool condition, [Localizable(false)] string unformattedMessage, params object?[] args)
        {
            if (!condition)
            {
                Fail(PrivateErrorHelpers.Format(unformattedMessage, args));
            }
        }
    }
}