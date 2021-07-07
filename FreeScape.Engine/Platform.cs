using System.Runtime.InteropServices;

namespace FreeScape.Engine
{
    public class Platform
    {
        [DllImport("X11")]
        extern public static int XInitThreads();
    }
}