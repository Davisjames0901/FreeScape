using System.Runtime.InteropServices;

namespace FreeScape.Engine
{
    internal static class Platform
    {
        [DllImport("X11")]
        internal static extern int XInitThreads();
    }
}