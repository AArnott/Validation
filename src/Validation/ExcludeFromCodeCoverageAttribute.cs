// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

using System;

#if NET35

#pragma warning disable SA1600 // Elements should be documented

namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, AllowMultiple = false, Inherited = false)]
internal class ExcludeFromCodeCoverageAttribute : Attribute
{
}

#endif
