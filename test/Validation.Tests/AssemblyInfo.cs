// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

using System.Resources;
using Xunit;

// Suppress xunit parallelizing tests since we manipulate statics (TraceListeners)
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]
[assembly: NeutralResourcesLanguage("en-US")]
