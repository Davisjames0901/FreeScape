using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FreeScape.Engine.Tests
{
    public class AccurateWaiter
    {
        public static void WaitMs(int ms)
        {
            var timer = Stopwatch.StartNew();
            while (true)
            {
                if (timer.ElapsedMilliseconds == ms)
                    break;
            }
        }
    }
}
