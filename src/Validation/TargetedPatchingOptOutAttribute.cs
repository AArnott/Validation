// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

#if NET35

#pragma warning disable SA1600 // Elements should be documented

namespace System.Runtime;

[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
internal sealed class TargetedPatchingOptOutAttribute : Attribute
{
    public TargetedPatchingOptOutAttribute(string reason)
    {
        this.Reason = reason;
    }

    public string Reason { get; }
}

#endif
