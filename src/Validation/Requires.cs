// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

namespace Validation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime;
    using System.Threading.Tasks;

    /// <summary>
    /// Common runtime checks that throw ArgumentExceptions upon failure.
    /// </summary>
    public static class Requires
    {
        /// <summary>
        /// Throws an ArgumentException if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, string? message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        /// <summary>
        /// Throws an ArgumentException if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, string message, object? arg1)
        {
            if (!condition)
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(message, arg1), parameterName);
            }
        }

        /// <summary>
        /// Throws an ArgumentException if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, string message, object? arg1, object? arg2)
        {
            if (!condition)
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(message, arg1, arg2), parameterName);
            }
        }

        /// <summary>
        /// Throws an ArgumentException if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, string message, params object?[] args)
        {
            if (!condition)
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(message, args), parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="System.ComponentModel.InvalidEnumArgumentException"/> if a given value is not a named value of the enum type.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum that may define the <paramref name="value"/>.</typeparam>
        /// <param name="value">The value that may be named by <typeparamref name="TEnum"/>.</param>
        /// <param name="parameterName">The name of the parameter to include in the exception, if thrown.</param>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void Defined<TEnum>(TEnum value, string parameterName)
            where TEnum : struct, Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                if (typeof(int) == Enum.GetUnderlyingType(typeof(TEnum)))
                {
                    throw new System.ComponentModel.InvalidEnumArgumentException(parameterName, (int)(object)value, typeof(TEnum));
                }
                else
                {
                    throw new System.ComponentModel.InvalidEnumArgumentException(PrivateErrorHelpers.Format(Strings.Argument_NotEnum, parameterName, value, typeof(TEnum).FullName));
                }
            }
        }

        /// <summary>
        /// Throws an ArgumentException.
        /// </summary>
        /// <returns>Nothing, as this method always throws. The signature allows for "throwing" Fail so C# knows execution will stop.</returns>        [DebuggerStepThrough]
        [DoesNotReturn]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static Exception Fail(Exception? innerException, string unformattedMessage, params object?[] args)
        {
            throw new ArgumentException(PrivateErrorHelpers.Format(unformattedMessage, args), innerException);
        }

        /// <summary>
        /// Throws an ArgumentException.
        /// </summary>
        /// <returns>Nothing, as this method always throws. The signature allows for "throwing" Fail so C# knows execution will stop.</returns>        [DebuggerStepThrough]
        [DebuggerStepThrough]
        [DoesNotReturn]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static Exception Fail(string unformattedMessage, params object?[] args)
        {
            throw Fail(PrivateErrorHelpers.Format(unformattedMessage, args));
        }

        /// <summary>
        /// Throws an ArgumentException.
        /// </summary>
        /// <returns>Nothing, as this method always throws. The signature allows for "throwing" Fail so C# knows execution will stop.</returns>        [DebuggerStepThrough]
        [DebuggerStepThrough]
        [DoesNotReturn]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static Exception Fail(string? message)
        {
            throw new ArgumentException(message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if a condition does not evaluate to true.
        /// </summary>
        /// <returns>Nothing, as this method always throws. The signature allows for "throwing" Fail so C# knows execution will stop.</returns>        [DebuggerStepThrough]
        [DebuggerStepThrough]
        [DoesNotReturn]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static Exception FailRange(string? parameterName, string? message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
            else
            {
                throw new ArgumentOutOfRangeException(parameterName, message);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the specified parameter's value is equal to the
        /// default value of the <see cref="Type"/> <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <c>null</c> or empty.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotDefault<T>(T? value, string parameterName)
            where T : struct
        {
            var defaultValue = default(T);
            if (defaultValue.Equals(value))
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(Strings.Argument_StructIsDefault, parameterName, typeof(T).FullName), parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is <see cref="Guid.Empty"/>.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is an empty guid (<see cref="Guid.Empty"/>.)</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotEmpty(Guid value, string? parameterName)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(Strings.Argument_EmptyGuid, parameterName), parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is IntPtr.Zero.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <returns>The value of the parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see cref="IntPtr.Zero"/>.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static IntPtr NotNull(IntPtr value, string? parameterName)
        {
            if (value == IntPtr.Zero)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method allows async methods to use Requires.NotNull without having to assign the result
        /// to local variables to avoid C# warnings.
        /// </remarks>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotNull([ValidatedNotNull, NotNull] Task? value, string? parameterName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null.
        /// </summary>
        /// <typeparam name="T">The type of the return value of the task.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method allows async methods to use Requires.NotNull without having to assign the result
        /// to local variables to avoid C# warnings.
        /// </remarks>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotNull<T>([ValidatedNotNull, NotNull] Task<T>? value, string? parameterName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <returns>The value of the parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static T? NotNull<T>([ValidatedNotNull, NotNull] T? value, string? parameterName)
            where T : class // ensures value-types aren't passed to a null checking method
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <returns>The value of the parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
        /// <remarks>
        /// This method exists for callers who themselves only know the type as a generic parameter which
        /// may or may not be a class, but certainly cannot be null.
        /// </remarks>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static T NotNullAllowStructs<T>([ValidatedNotNull, NotNull, AllowNull] T value, string? parameterName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null,
        /// has no elements or has an element with a null value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotNullEmptyOrNullElements<T>([ValidatedNotNull, NotNull] IEnumerable<T?>? values, string? parameterName)
            where T : class // ensures value-types aren't passed to a null checking method
        {
            NotNull(values, parameterName);

            bool hasElements = false;
            foreach (T? value in values)
            {
                hasElements = true;

                if (value is null)
                {
                    throw new ArgumentException(PrivateErrorHelpers.Format(Strings.Argument_NullElement, parameterName), parameterName);
                }
            }

            if (!hasElements)
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(Strings.Argument_EmptyArray, parameterName), parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null or empty.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <c>null</c> or empty.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotNullOrEmpty([ValidatedNotNull, NotNull] string? value, string? parameterName)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as earlier as possible.
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (value.Length == 0 || value[0] == '\0')
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(Strings.Argument_EmptyString, parameterName), parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null,
        /// has no elements.
        /// </summary>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotNullOrEmpty([ValidatedNotNull, NotNull] IEnumerable? values, string? parameterName)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as earlier as possible.
            if (values is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (!values.GetEnumerator().MoveNext())
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(Strings.Argument_EmptyArray, parameterName), parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null,
        /// has no elements.
        /// </summary>
        /// <typeparam name="T">The type produced by the enumeration.</typeparam>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotNullOrEmpty<T>([ValidatedNotNull, NotNull] IEnumerable<T>? values, string? parameterName)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as earlier as possible.
            if (values is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            using IEnumerator<T> e = values.GetEnumerator();
            if (!e.MoveNext())
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(Strings.Argument_EmptyArray, parameterName), parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is null, empty, or whitespace.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <c>null</c> or empty.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotNullOrWhiteSpace([ValidatedNotNull, NotNull] string? value, string? parameterName)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as earlier as possible.
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (value.Length == 0 || value[0] == '\0')
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(Strings.Argument_EmptyString, parameterName), parameterName);
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(Strings.Argument_Whitespace, parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is not null
        /// <em>and</em> has an element with a null value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NullOrNotNullElements<T>(IEnumerable<T>? values, string? parameterName)
        {
            if (values is object)
            {
                foreach (T value in values)
                {
                    if (value is null)
                    {
                        throw new ArgumentException(PrivateErrorHelpers.Format(Strings.Argument_NullElement, parameterName), parameterName);
                    }
                }
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void Range([DoesNotReturnIf(false)] bool condition, string? parameterName, string? message = null)
        {
            if (!condition)
            {
                FailRange(parameterName, message);
            }
        }

        /// <summary>
        /// Validates some expression describing the acceptable condition for an argument evaluates to true.
        /// </summary>
        /// <param name="condition">The expression that must evaluate to true to avoid an <see cref="ArgumentException"/>.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="unformattedMessage">The unformatted message.</param>
        /// <param name="args">Formatting arguments.</param>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void That(bool condition, string? parameterName, string unformattedMessage, params object?[] args)
        {
            if (!condition)
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(unformattedMessage, args), parameterName);
            }
        }

        /// <summary>
        /// Validates some expression describing the acceptable condition for an argument evaluates to true.
        /// </summary>
        /// <param name="condition">The expression that must evaluate to true to avoid an <see cref="InvalidOperationException"/>.</param>
        /// <param name="message">The message to include with the exception.</param>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void ValidState(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}