﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

namespace Validation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Resources;
    using System.Runtime;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Common runtime checks that throw ArgumentExceptions upon failure.
    /// </summary>
    public static class Requires
    {
        /// <summary>
        /// Throws an exception if the specified parameter's value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <returns>The value of the parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static T NotNull<T>([ValidatedNotNull, NotNull] T value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
            where T : class // ensures value-types aren't passed to a null checking method
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is IntPtr.Zero.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <returns>The value of the parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see cref="IntPtr.Zero"/>.</exception>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static IntPtr NotNull(IntPtr value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        {
            if (value == IntPtr.Zero)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

#if !NET35
        /// <summary>
        /// Throws an exception if the specified parameter's value is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method allows async methods to use Requires.NotNull without having to assign the result
        /// to local variables to avoid C# warnings.
        /// </remarks>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotNull([ValidatedNotNull, NotNull] System.Threading.Tasks.Task value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of the return value of the task.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method allows async methods to use Requires.NotNull without having to assign the result
        /// to local variables to avoid C# warnings.
        /// </remarks>
        [DebuggerStepThrough]
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static void NotNull<T>([ValidatedNotNull, NotNull] System.Threading.Tasks.Task<T> value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
#endif

        /// <summary>
        /// Throws an exception if the specified parameter's value is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <returns>The value of the parameter.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method exists for callers who themselves only know the type as a generic parameter which
        /// may or may not be a class, but certainly cannot be <see langword="null"/>.
        /// </remarks>
        [DebuggerStepThrough]
        public static T NotNullAllowStructs<T>([ValidatedNotNull, NotNull] T value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is <see langword="null"/> or empty.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <see langword="null"/> or empty.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty([ValidatedNotNull, NotNull] string value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as early as possible.
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (value.Length == 0 || value[0] == '\0')
            {
                throw new ArgumentException(Format(Strings.Argument_EmptyString, parameterName), parameterName);
            }
        }

#if !NET35
        /// <summary>
        /// Throws an exception if the specified parameter's value is <see langword="null"/>, empty, or whitespace.
        /// </summary>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <see langword="null"/> or empty.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrWhiteSpace([ValidatedNotNull, NotNull] string value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as early as possible.
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (value.Length == 0 || value[0] == '\0')
            {
                throw new ArgumentException(Format(Strings.Argument_EmptyString, parameterName), parameterName);
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(Strings.Argument_Whitespace, parameterName);
            }
        }
#endif

        /// <summary>
        /// Throws an exception if the specified parameter's value is <see langword="null"/>,
        /// has no elements.
        /// </summary>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty([ValidatedNotNull, NotNull] IEnumerable values, [CallerArgumentExpression(nameof(values))] string? parameterName = null)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as early as possible.
            if (values is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            IEnumerator enumerator = values.GetEnumerator();
            using (enumerator as IDisposable)
            {
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentException(Format(Strings.Argument_EmptyArray, parameterName), parameterName);
                }
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is <see langword="null"/>,
        /// has no elements.
        /// </summary>
        /// <typeparam name="T">The type produced by the enumeration.</typeparam>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        public static void NotNullOrEmpty<T>([ValidatedNotNull, NotNull] IEnumerable<T> values, [CallerArgumentExpression(nameof(values))] string? parameterName = null)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as early as possible.
            if (values is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            using IEnumerator<T> enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                throw new ArgumentException(Format(Strings.Argument_EmptyArray, parameterName), parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is <see langword="null"/>,
        /// has no elements or has an element with a <see langword="null"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        public static void NotNullEmptyOrNullElements<T>([ValidatedNotNull, NotNull] IEnumerable<T> values, [CallerArgumentExpression(nameof(values))] string? parameterName = null)
            where T : class // ensures value-types aren't passed to a null checking method
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as early as possible.
            if (values is null)
            {
                throw new ArgumentNullException(parameterName);
            }

            bool hasElements = false;
            foreach (T? value in values)
            {
                hasElements = true;

                if (value is null)
                {
                    throw new ArgumentException(Format(Strings.Argument_NullElement, parameterName), parameterName);
                }
            }

            if (!hasElements)
            {
                throw new ArgumentException(Format(Strings.Argument_EmptyArray, parameterName), parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if the specified parameter's value is not <see langword="null"/>
        /// <em>and</em> has an element with a <see langword="null"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="values">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        [DebuggerStepThrough]
        public static void NullOrNotNullElements<T>(IEnumerable<T> values, [CallerArgumentExpression(nameof(values))] string? parameterName = null)
        {
            if (values is object)
            {
                foreach (T value in values)
                {
                    if (value is null)
                    {
                        throw new ArgumentException(Format(Strings.Argument_NullElement, parameterName), parameterName);
                    }
                }
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void Range([DoesNotReturnIf(false)] bool condition, string? parameterName, string? message = null)
        {
            if (!condition)
            {
                FailRange(parameterName, message);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if a condition does not evaluate to true.
        /// </summary>
        /// <returns>Nothing.  This method always throws.</returns>
        [DebuggerStepThrough]
        [DoesNotReturn]
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
        /// Throws an <see cref="ArgumentException"/> if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, string? message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, string message, object? arg1)
        {
            if (!condition)
            {
                throw new ArgumentException(Format(message, arg1), parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, string message, object? arg1, object? arg2)
        {
            if (!condition)
            {
                throw new ArgumentException(Format(message, arg1, arg2), parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if a condition does not evaluate to true.
        /// </summary>
        [DebuggerStepThrough]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, string message, params object?[] args)
        {
            if (!condition)
            {
                throw new ArgumentException(Format(message, args), parameterName);
            }
        }

#if NET9_0_OR_GREATER
        /// <inheritdoc cref="Argument(bool, string?, string, object?[])"/>"
        [DebuggerStepThrough]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, string message, params ReadOnlySpan<object?> args)
        {
            if (!condition)
            {
                throw new ArgumentException(Format(message, args), parameterName);
            }
        }
#endif

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if a condition does not evaluate to true.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="parameterName">The name of the parameter to blame in the exception, if thrown.</param>
        /// <param name="resourceManager">The resource manager from which to retrieve the exception message. For example: <c>Strings.ResourceManager</c>.</param>
        /// <param name="resourceName">The name of the string resource to obtain for the exception message. For example: <c>nameof(Strings.SomeError)</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="resourceManager"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="condition"/> is <see langword="false"/>.</exception>
        /// <remarks>
        /// This overload allows only loading a localized string in the error condition as an optimization in perf critical sections over the simpler
        /// to use <see cref="Argument(bool, string?, string?)"/> overload.
        /// </remarks>
        [DebuggerStepThrough]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, ResourceManager resourceManager, string resourceName)
        {
            NotNull(resourceManager, nameof(resourceManager));
            if (!condition)
            {
                throw new ArgumentException(resourceManager.GetString(resourceName, CultureInfo.CurrentCulture), parameterName);
            }
        }

        /// <inheritdoc cref="Argument(bool, string?, ResourceManager, string, object?, object?)"/>
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, ResourceManager resourceManager, string unformattedMessageResourceName, object? arg1)
        {
            NotNull(resourceManager, nameof(resourceManager));
            if (!condition)
            {
                throw new ArgumentException(Format(resourceManager, unformattedMessageResourceName, arg1), parameterName);
            }
        }

        /// <inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/summary"/>
        /// <inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/remarks"/>
        /// <inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/exception"/>
        /// <param name="unformattedMessageResourceName">The name of the string resource to obtain for the exception message. For example: <c>nameof(Strings.SomeError)</c>.</param>
        /// <param name="condition"><inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/param[@name='condition']"/></param>
        /// <param name="parameterName"><inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/param[@name='parameterName']"/></param>
        /// <param name="resourceManager"><inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/param[@name='resourceManager']"/></param>
        /// <param name="arg1">The first formatting argument.</param>
        /// <param name="arg2">The second formatting argument.</param>
        [DebuggerStepThrough]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, ResourceManager resourceManager, string unformattedMessageResourceName, object? arg1, object? arg2)
        {
            NotNull(resourceManager, nameof(resourceManager));
            if (!condition)
            {
                throw new ArgumentException(Format(resourceManager, unformattedMessageResourceName, arg1, arg2), parameterName);
            }
        }

        /// <inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/summary"/>
        /// <inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/remarks"/>
        /// <inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/exception"/>
        /// <param name="unformattedMessageResourceName">The name of the string resource to obtain for the exception message. For example: <c>nameof(Strings.SomeError)</c>.</param>
        /// <param name="condition"><inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/param[@name='condition']"/></param>
        /// <param name="parameterName"><inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/param[@name='parameterName']"/></param>
        /// <param name="resourceManager"><inheritdoc cref="Argument(bool, string?, ResourceManager, string)" path="/param[@name='resourceManager']"/></param>
        /// <param name="args">The formatting arguments.</param>
        [DebuggerStepThrough]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, ResourceManager resourceManager, string unformattedMessageResourceName, params object?[] args)
        {
            NotNull(resourceManager, nameof(resourceManager));
            if (!condition)
            {
                throw new ArgumentException(Format(resourceManager, unformattedMessageResourceName, args), parameterName);
            }
        }

#if NET9_0_OR_GREATER
        /// <inheritdoc cref="Argument(bool, string?, ResourceManager, string, object?[])"/>
        [DebuggerStepThrough]
        public static void Argument([DoesNotReturnIf(false)] bool condition, string? parameterName, ResourceManager resourceManager, string unformattedMessageResourceName, params ReadOnlySpan<object?> args)
        {
            NotNull(resourceManager, nameof(resourceManager));
            if (!condition)
            {
                throw new ArgumentException(Format(resourceManager, unformattedMessageResourceName, args), parameterName);
            }
        }
#endif

        /// <summary>
        /// Throws an ArgumentException.
        /// </summary>
        /// <returns>Nothing.  It always throws.</returns>
        [DebuggerStepThrough]
        [DoesNotReturn]
        public static Exception Fail(string? message)
        {
            throw new ArgumentException(message);
        }

        /// <summary>
        /// Throws an ArgumentException.
        /// </summary>
        /// <returns>Nothing.  It always throws.</returns>
        [DebuggerStepThrough]
        [DoesNotReturn]
        public static Exception Fail(string unformattedMessage, params object?[] args)
        {
            throw Fail(Format(unformattedMessage, args));
        }

#if NET9_0_OR_GREATER
        /// <inheritdoc cref="Fail(string, object?[])"/>
        [DebuggerStepThrough]
        [DoesNotReturn]
        public static Exception Fail(string unformattedMessage, params ReadOnlySpan<object?> args)
        {
            throw Fail(Format(unformattedMessage, args));
        }
#endif

        /// <summary>
        /// Throws an ArgumentException.
        /// </summary>
        /// <returns>Nothing. This method always throws.</returns>
        [DebuggerStepThrough]
        [DoesNotReturn]
        public static Exception Fail(Exception? innerException, string unformattedMessage, params object?[] args)
        {
            throw new ArgumentException(Format(unformattedMessage, args), innerException);
        }

#if NET9_0_OR_GREATER
        /// <inheritdoc cref="Fail(Exception, string, object?[])"/>
        [DebuggerStepThrough]
        [DoesNotReturn]
        public static Exception Fail(Exception? innerException, string unformattedMessage, params ReadOnlySpan<object?> args)
        {
            throw new ArgumentException(Format(unformattedMessage, args), innerException);
        }
#endif

#if !NET35
        /// <summary>
        /// Throws an <see cref="InvalidEnumArgumentException"/> if a given value is not a named value of the enum type.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum that may define the <paramref name="value"/>.</typeparam>
        /// <param name="value">The value that may be named by <typeparamref name="TEnum"/>.</param>
        /// <param name="parameterName">The name of the parameter to include in the exception, if thrown. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        [DebuggerStepThrough]
        public static void Defined<TEnum>(TEnum value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
            where TEnum : struct, Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                if (typeof(int) == typeof(TEnum).GetEnumUnderlyingType())
                {
                    throw new InvalidEnumArgumentException(parameterName, (int)(object)value, typeof(TEnum));
                }
                else
                {
                    throw new InvalidEnumArgumentException(Format(Strings.Argument_NotEnum, parameterName, value, typeof(TEnum).FullName));
                }
            }
        }
#endif

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the specified parameter's value is equal to the
        /// default value of the <see cref="Type"/> <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value of the argument.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception. If this argument is omitted (explicitly writing <see langword="null" /> does not qualify), the expression used in the first argument will be used as the parameter name.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is <see langword="null"/> or empty.</exception>
        [DebuggerStepThrough]
        public static void NotDefault<T>(T value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
            where T : struct
        {
            var defaultValue = default(T);
            if (defaultValue.Equals(value))
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(Strings.Argument_StructIsDefault, parameterName, typeof(T).FullName), parameterName);
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
        [Obsolete("Use Argument(bool, string, string, object[]) instead.")]
        public static void That([DoesNotReturnIf(false)] bool condition, string? parameterName, string unformattedMessage, params object?[] args)
        {
            if (!condition)
            {
                throw new ArgumentException(PrivateErrorHelpers.Format(unformattedMessage, args), parameterName);
            }
        }

        /// <summary>
        /// Throws an exception if <paramref name="values"/> is <see langword="null"/>,
        /// <paramref name="predicate"/> is <see langword="null"/>, or if <paramref name="values"/> is not <see langword="null"/>
        /// <em>and</em> has an element which does not match the given predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="values">The value of the argument.</param>
        /// <param name="predicate">The predicate used to test the elements.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <param name="message">A message to be used in the resulting exception.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="predicate"/> or <paramref name="values"/> is <see langword="null"/>.</exception>
        [DebuggerStepThrough]
        public static void ValidElements<T>([ValidatedNotNull] IEnumerable<T> values, Predicate<T> predicate, string? parameterName, string? message)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as early as possible.
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T value in values)
            {
                if (!predicate(value))
                {
                    throw new ArgumentException(message, parameterName);
                }
            }
        }

        /// <summary>
        /// Throws an exception if <paramref name="values"/> is <see langword="null"/>,
        /// <paramref name="predicate"/> is <see langword="null"/>, or if <paramref name="values"/> is not <see langword="null"/>
        /// <em>and</em> has an element which does not match the given predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="values">The value of the argument.</param>
        /// <param name="predicate">The predicate used to test the elements.</param>
        /// <param name="parameterName">The name of the parameter to include in any thrown exception.</param>
        /// <param name="unformattedMessage">The unformatted message.</param>
        /// <param name="args">Formatting arguments.</param>
        /// <exception cref="ArgumentException">Thrown if the tested condition is false.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="predicate"/> is <see langword="null"/>.</exception>
        [DebuggerStepThrough]
        public static void ValidElements<T>([ValidatedNotNull] IEnumerable<T> values, Predicate<T> predicate, string? parameterName, string unformattedMessage, params object?[] args)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as early as possible.
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T value in values)
            {
                if (!predicate(value))
                {
                    throw new ArgumentException(PrivateErrorHelpers.Format(unformattedMessage, args), parameterName);
                }
            }
        }

#if NET9_0_OR_GREATER
        /// <inheritdoc cref="ValidElements{T}(IEnumerable{T}, Predicate{T}, string?, string, object?[])"/>
        [DebuggerStepThrough]
        public static void ValidElements<T>([ValidatedNotNull] IEnumerable<T> values, Predicate<T> predicate, string? parameterName, string unformattedMessage, params ReadOnlySpan<object?> args)
        {
            // To whoever is doing random code cleaning:
            // Consider the performance when changing the code to delegate to NotNull.
            // In general do not chain call to another function, check first and return as early as possible.
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T value in values)
            {
                if (!predicate(value))
                {
                    throw new ArgumentException(PrivateErrorHelpers.Format(unformattedMessage, args), parameterName);
                }
            }
        }

#endif

        /// <summary>
        /// Validates some expression describing the acceptable condition for an argument evaluates to true.
        /// </summary>
        /// <param name="condition">The expression that must evaluate to true to avoid an <see cref="InvalidOperationException"/>.</param>
        /// <param name="message">The message to include with the exception.</param>
        [DebuggerStepThrough]
        [Obsolete("Use Verify.Operation(bool, string) instead.")]
        public static void ValidState(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// Helper method that formats string arguments.
        /// </summary>
#if NET9_0_OR_GREATER
        private static string Format(string format, params ReadOnlySpan<object?> arguments)
#else
        private static string Format(string format, params object?[] arguments)
#endif
        {
            return PrivateErrorHelpers.Format(format, arguments);
        }

#if NET9_0_OR_GREATER
        private static string Format(ResourceManager resourceManager, string resourceName, params ReadOnlySpan<object?> arguments)
#else
        private static string Format(ResourceManager resourceManager, string resourceName, params object?[] arguments)
#endif
        {
            return Format(resourceManager.GetString(resourceName, CultureInfo.CurrentCulture) ?? $"Missing resource named {resourceManager}", arguments);
        }
    }
}
