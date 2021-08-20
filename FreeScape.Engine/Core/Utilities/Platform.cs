using System.Runtime.InteropServices;

namespace FreeScape.Engine.Core.Utilities
{
    internal static class Platform
    {
        [DllImport("X11")]
        internal static extern int XInitThreads();
    }
}