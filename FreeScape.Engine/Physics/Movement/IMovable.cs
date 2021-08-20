using System.Numerics;
using FreeScape.Engine.Core.GameObjects;

namespace FreeScape.Engine.Physics.Movement
{
    public interface IMovable : IGameObject
    {
        float Speed { get; }
        HeadingVector HeadingVector { get; }
        new Vector2 Position { get; set; }
    }
}
