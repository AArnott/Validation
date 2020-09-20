// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

#if NET20 || NETSTANDARD1_0 || NETSTANDARD1_3
namespace System.Runtime
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
    public sealed class AssemblyTargetedPatchBandAttribute : Attribute
    {
        public AssemblyTargetedPatchBandAttribute(string targetedPatchBand)
        {
            TargetedPatchBand = targetedPatchBand;
        }

        public string TargetedPatchBand { get; }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public sealed class TargetedPatchingOptOutAttribute : Attribute
    {
        public TargetedPatchingOptOutAttribute(string reason)
        {
            Reason = reason;
        }

        public string Reason { get; }
    }
}
#endif