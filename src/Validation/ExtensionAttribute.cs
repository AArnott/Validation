// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

#if NET20

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Allows the C# compiler to target .NET 2.0 while still using extension method syntax.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class ExtensionAttribute : Attribute
    {
    }
}

#endif
