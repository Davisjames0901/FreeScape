using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Utilities;
using SFML.Graphics;
using SFML.System;

namespace FreeScape.Engine.Render
{
    public class Perspective : ITickable
    {
        private readonly Vector2f _screenSize;
        private IGameObject _target;
        public Perspective(string name, Vector2f center, Vector2f size, float scale)
        {
            _screenSize = size;
            Name = name;
            View = new View(center, size/scale);
        }
        
        public string Name { get; }
        public View View { get; }
        public float Scaling => _screenSize.X / View.Size.X;
        public Vector2f Corner => (View.Center - View.Size / 2)*Scaling;

        public void Track(IGameObject go)
        {
            _target = go;
        }
        public void Tick()
        {
            if (_target != null)
            {
                View.Center = Maths.Lerp(View.Center, _target.Position, 0.01f, 0.1f);
            }
        }
    }
}