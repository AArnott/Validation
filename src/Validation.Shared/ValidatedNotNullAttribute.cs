//-----------------------------------------------------------------------
// <copyright file="ValidatedNotNullAttribute.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Validation
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Indicates to Code Analysis that a method validates a particular parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class ValidatedNotNullAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatedNotNullAttribute"/> class.
        /// </summary>
        public ValidatedNotNullAttribute()
        {
        }
    }
}
