using System;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Utilities;

namespace FreeScape.Engine.Physics.Movements
{
    public class KeyboardMovement : IMovement
    {
        public readonly ActionProvider ActionProvider;
        
        public HeadingVector HeadingVector { get; }

        public KeyboardMovement(ActionProvider actionProvider)
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
            Console.WriteLine($"{up} {down} {left} {right}");

            HeadingVector.UpdateHeadingVector(Maths.GetHeadingVectorFromMovement(up, down, left, right));
        }
    }
}