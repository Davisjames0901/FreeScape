using FreeScape.Engine.Core.Utilities;
using FreeScape.Engine.Physics.Movement;

namespace FreeScape.Engine.Input.Controllers
{
    public class KeyboardController : IController
    {
        public readonly ActionProvider ActionProvider;
        
        public HeadingVector HeadingVector { get; }

        public KeyboardController(ActionProvider actionProvider)
        {
            ActionProvider = actionProvider;
            HeadingVector = new HeadingVector();
        }
        
        public void Tick()
        {
            var up = ActionProvider.IsActionActivated("MoveUp");
            var down = ActionProvider.IsActionActivated("MoveDown");
            var left = ActionProvider.IsActionActivated("MoveLeft");
            var right = ActionProvider.IsActionActivated("MoveRight");

            HeadingVector.UpdateHeadingVector(Maths.GetHeadingVectorFromMovement(up, down, left, right));
        }
    }
}