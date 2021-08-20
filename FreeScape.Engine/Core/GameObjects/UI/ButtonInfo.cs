using System;
using System.Numerics;

namespace FreeScape.Engine.Core.GameObjects.UI
{
    [Obsolete("We should be using the normal texture providing and do the other functions in code. Maybe look into the animation provider for wiggle")]
    public class ButtonInfo
    {
        public Vector2 Position;
        public Vector2 Size;
        public string ButtonTexture;
        public System.Action OnClickAction;
        public bool Wigglable = false;
    }
}
