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
        private readonly Vector2 _screenSize;
        private IGameObject _target;
        public Perspective(string name, Vector2 center, Vector2 size, float scale)
        {
            _screenSize = size;
            Name = name;
            View = new View(center, size/scale);
        }
        
        public string Name { get; }
        public View View { get; }
        public float Scaling => _screenSize.X / View.Size.X;
        public Vector2 Corner => (View.Center - View.Size / 2)*Scaling;

        public void Track(IGameObject go)
        {
            _target = go;
        }
        public void Tick()
        {
            if (_target != null)
            {
                float speed = Scaling / 150;
                View.Center = Maths.Lerp(View.Center, _target.Position, speed, 0.1f);
            }
        }
    }
}