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
        private IGameObject _target;
        public Vector2? TargetPosition => _target?.Position;
        private float? _trackSharpness;
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
        public float ScreenScaling => _screenSize.X / ScreenView.Size.X;
        public Vector2 WorldCorner => (WorldView.Center - WorldView.Size / 2)*WorldScaling;

        public void SetCenter(Vector2 position)
        {
            WorldView.Center = position;
        }

        public void Track(IGameObject go, float sharpness = 0.5f)
        {
            _target = go;
            _trackSharpness = sharpness;
        }

        public void Track(Vector2 position, float sharpness)
        {
            _target = new EmptyGameObject(position);
            _trackSharpness = sharpness;
        }
        
        public void Tick()
        {
            if (_target != null)
            {
                var blend = 1 - Math.Pow(1f - (float)_trackSharpness, _frameTime.DeltaTimeMilliSeconds);
                WorldView.Center = WorldView.Center.Lerp(_target.Position, (float)blend, 0.1f);
            }
        }
    }
}