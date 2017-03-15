// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

#if NET20

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Allows the C# compiler to target .NET 2.0 while still using extension method syntax.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Method)]
    internal class ExtensionAttribute : Attribute
    {
    }
}

#endif
