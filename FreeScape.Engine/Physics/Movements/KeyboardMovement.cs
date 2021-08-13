using FreeScape.Engine.Providers;
using FreeScape.Engine.Utilities;

namespace FreeScape.Engine.Physics.Movements
{
    public class KeyboardMovement : IMovement
    {
        private readonly ActionProvider _actionProvider;
        
        public HeadingVector HeadingVector { get; private set; }

        public KeyboardMovement(ActionProvider actionProvider)
        {
            _actionProvider = actionProvider;
        }
        
        public void Tick()
        {
            var up = _actionProvider.IsActionActivated("MoveUp");
            var down = _actionProvider.IsActionActivated("MoveDown");
            var left = _actionProvider.IsActionActivated("MoveLeft");
            var right = _actionProvider.IsActionActivated("MoveRight");

            HeadingVector.UpdateHeadingVector(Maths.GetHeadingVectorFromMovement(up, down, left, right));
        }
    }
}