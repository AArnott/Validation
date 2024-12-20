// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

namespace Validation
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    /// <content>
    /// Contains the inner exception thrown by Assumes.
    /// </content>
    public partial class Assumes
    {
        /// <summary>
        /// The exception that is thrown when an internal assumption failed.
        /// </summary>
        [Serializable]
        [SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic", Justification = "Internal exceptions should not be caught.")]
        [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "This is an internal exception type and we don't use the recommended ctors.")]
        private sealed class InternalErrorException : Exception
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="InternalErrorException"/> class.
            /// </summary>
            [DebuggerStepThrough]
            public InternalErrorException(string? message = null)
                : base(message ?? Strings.InternalExceptionMessage)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InternalErrorException"/> class.
            /// </summary>
            [DebuggerStepThrough]
            public InternalErrorException(string? message, Exception? innerException)
                : base(message ?? Strings.InternalExceptionMessage, innerException)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InternalErrorException"/> class.
            /// </summary>
            [DebuggerStepThrough]
#if NET
            [Obsolete]
#endif
            private InternalErrorException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }
        }
    }
}
