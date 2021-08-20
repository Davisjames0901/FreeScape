using FreeScape.Engine.Physics.Movement;

namespace FreeScape.Engine.Input.Controllers
{
    public interface IController
    {
        HeadingVector HeadingVector { get; }
        void Tick();
    }
}