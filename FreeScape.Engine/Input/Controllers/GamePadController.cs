using FreeScape.Engine.Physics.Movement;

namespace FreeScape.Engine.Input.Controllers
{
    public class GamePadController : IController
    {
        public GamePadController()
        {
            HeadingVector = new HeadingVector();
        }
        public HeadingVector HeadingVector { get; }
        
        public void Tick()
        {
                
        }
    }
}