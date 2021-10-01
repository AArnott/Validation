// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the Ms-PL license. See LICENSE.txt file in the project root for full license information.

// Ensure the tests defined in this file always emulate a client compiled for Debug
#define DEBUG

using System;
using System.Diagnostics;

public class TestingTraceListener : DefaultTraceListener
{
    public override void Fail(string? message, string? detailMessage)
    {
        throw new Exception(message + detailMessage);
    }
}
