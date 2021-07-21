using System;
using System.Numerics;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Utilities;
using SFML.Graphics;

namespace FreeScape.Engine.Render
{
    public class Perspective : ITickable
    {
        private readonly FrameTimeProvider _frameTime;

        private readonly Vector2 _screenSize;
        private Vector2? _target;
        public Vector2? TargetPosition => _target;
        private float? _trackSpeed;
        public Perspective(string name, Vector2 center, Vector2 size, float scale, FrameTimeProvider frameTime)
        {
            _frameTime = frameTime;
            _screenSize = size;
            Name = name;
            WorldView = new View(center, size/scale);
            ScreenView = new View(Vector2.Zero, size/scale);
        }
        
        public string Name { get; }
        public View WorldView { get; }
        public View ScreenView { get; }
        public float WorldScaling => _screenSize.X / WorldView.Size.X;
        public Vector2 WorldCorner => (WorldView.Center - WorldView.Size / 2)*WorldScaling;

        public void SetCenter(Vector2 position)
        {
            WorldView.Center = position;
        }

        public void Track(IGameObject go)
        {
            _target = go.Position;
            _trackSpeed = null;
        }

        public void Track(Vector2 position, float speed)
        {
            Console.WriteLine($"Tracking: {position}, at speed: {speed}");
            _target = position;
            _trackSpeed = speed;
        }
        
        public void Tick()
        {
            if (_target != null)
            {
                float speed = WorldScaling / (_trackSpeed ?? 4.5f);
                var dt = (float)_frameTime.DeltaTimeMilliSeconds;
                speed = (float)(1 - Math.Pow((double)speed, dt));
                WorldView.Center = Maths.Lerp(WorldView.Center, _target.Value, speed, 0.1f);
            }
        }
    }
}