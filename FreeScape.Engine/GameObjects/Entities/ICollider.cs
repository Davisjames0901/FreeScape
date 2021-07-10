using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.GameObjects.Entities
{
    public interface ICollider
    {
        public Vector2 Position { get; }
        public Vector2 ColliderSize { get; }
    }
}
