using FreeScape.Engine.Providers;
using SFML.System;
using System;

namespace FreeScape.Engine.Physics
{
    public class Movement
    {
        FrameTimeProvider _frameTime;
        public Movement(FrameTimeProvider frameTime)
        {
            _frameTime = frameTime;
        }
        public Vector2f BasicMove(Vector2f location, Vector2f velocity)
        {
            float deltaTime = (float)_frameTime.DeltaTimeMilliSeconds;
            Console.WriteLine(deltaTime);

            return new Vector2f(location.X + ( velocity.X * deltaTime), location.Y + (velocity.Y * deltaTime));
        }
    }

}
