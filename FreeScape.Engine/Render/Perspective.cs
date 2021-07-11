using System;
using System.Numerics;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Utilities;
using SFML.Graphics;
using SFML.System;

namespace FreeScape.Engine.Render
{
    public class Perspective : ITickable
    {
        private readonly FrameTimeProvider _frameTime;

        private readonly Vector2 _screenSize;
        private IGameObject _target;
        public Perspective(string name, Vector2 center, Vector2 size, float scale, FrameTimeProvider frameTime)
        {
            _frameTime = frameTime;
            _screenSize = size;
            Name = name;
            View = new View(center, size/scale);
        }
        
        public string Name { get; }
        public View View { get; }
        public float Scaling => _screenSize.X / View.Size.X;
        public Vector2 Corner => (View.Center - View.Size / 2)*Scaling;

        public void SetCenter(Vector2 position)
        {
            View.Center = position;
        }

        public void Track(IGameObject go)
        {
            _target = go;
        }
        public void Tick()
        {
            if (_target != null)
            {
                float speed = Scaling / 150;
                var dt = (float)_frameTime.DeltaTimeMilliSeconds;
                speed = (float)(1 - Math.Pow((double)speed, dt));
                //speed -= dt;
                Console.WriteLine($"Speed : {speed}, deltaTime : {dt}");
                View.Center = Maths.Lerp(View.Center, _target.Position, speed, 0.1f);
            }
        }
    }
}