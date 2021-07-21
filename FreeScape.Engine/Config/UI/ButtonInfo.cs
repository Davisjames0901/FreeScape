using System.Numerics;

namespace FreeScape.Engine.Config.UI
{
    public class ButtonInfo
    {
        public Vector2 Position;
        public Vector2 Size;
        public string ButtonTexture;
        public System.Action OnClickAction;
        public bool Wigglable = false;
    }
}
