using System.Numerics;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Physics.Movements;

namespace FreeScape.Engine.Physics
{
    public interface IMovable : IGameObject
    {
        float Speed { get; }
        HeadingVector HeadingVector { get; }
        new Vector2 Position { get; set; }
    }
}
