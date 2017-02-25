// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

#if NET40 || Profile328

namespace Validation
{
    using System;

    /// <summary>
    /// Extension methods for the <see cref="Type"/> class to emulate newer reflection APIs.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Returns the specified type.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <returns>The type specified.</returns>
        /// <remarks>
        /// This silly method allows the same code to compile against the newer
        /// as well as older Reflection APIs.
        /// </remarks>
        internal static Type GetTypeInfo(this Type type) => type;
    }
}

#endif
