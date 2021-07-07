using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.GameObjects.Entities
{
    public interface ICollider
    {
        public Vector2f Position { get; }
        public Vector2f ColliderSize { get; }
    }
}
