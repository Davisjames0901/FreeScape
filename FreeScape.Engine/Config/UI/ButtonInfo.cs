using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.GameObjects.UI
{
    public class ButtonInfo
    {
        public string Name;
        public Vector2 Position;
        public Vector2 Size;
        public string ButtonTextureDefault;
        public string ButtonTextureHover;
        public Action OnClickAction;


    }
}
