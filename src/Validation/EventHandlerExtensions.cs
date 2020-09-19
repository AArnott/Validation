﻿// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

namespace Microsoft
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Validation;

    /// <summary>
    /// Extension methods to make it easier to safely invoke events.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "These aren't events.")]
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Invokes any event handlers that are hooked to the specified event.
        /// </summary>
        /// <param name="handler">The event.  Null is allowed.</param>
        /// <param name="sender">The value to pass as the sender of the event.</param>
        /// <param name="e">Event arguments to include.</param>
        public static void Raise(this Delegate? handler, object sender, EventArgs e)
        {
            Requires.NotNull(sender, nameof(sender));
            Requires.NotNull(e, nameof(e));

            handler?.DynamicInvoke(sender, e);
        }

        /// <summary>
        /// Invokes any event handlers that are hooked to the specified event.
        /// </summary>
        /// <param name="handler">The event.  Null is allowed.</param>
        /// <param name="sender">The value to pass as the sender of the event.</param>
        /// <param name="e">Event arguments to include.</param>
        public static void Raise(this EventHandler? handler, object sender, EventArgs e)
        {
            Requires.NotNull(sender, nameof(sender));
            Requires.NotNull(e, nameof(e));

            handler?.Invoke(sender, e);
        }

        /// <summary>
        /// Invokes any event handlers that are hooked to the specified event.
        /// </summary>
        /// <typeparam name="T">The type of EventArgs.</typeparam>
        /// <param name="handler">The event.  Null is allowed.</param>
        /// <param name="sender">The value to pass as the sender of the event.</param>
        /// <param name="e">Event arguments to include.</param>
        public static void Raise<T>(this EventHandler<T>? handler, object sender, T e)
            where T : EventArgs
        {
            Requires.NotNull(sender, nameof(sender));
            Requires.NotNullAllowStructs(e, nameof(e));

            handler?.Invoke(sender, e);
        }
    }
}