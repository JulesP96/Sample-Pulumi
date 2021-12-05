using Pulumi;
using System;
using System.Diagnostics;
using System.Threading;

namespace MeetupPulumi.Core
{
    public abstract class StackBase : Stack
    {
        protected StackBase()
        {
            bool.TryParse(Environment.GetEnvironmentVariable("PULUMI_DEBUG"), out var isDebug);

            if (!isDebug) return;

            while (!Debugger.IsAttached) Thread.Sleep(100);
        }
    }
}