using FreeScape.Engine.Config.TileSet;
using FreeScape.Engine.Providers;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.Render
{
    public class Animated : IAnimated, ITickable
    {
        public readonly AnimationProvider _animationProvider;
        private readonly MapProvider _mapProvider;
        public string TileSetName { get; set; }
        public Stopwatch Timer { get; set; }
        public AnimationFrame CurrentFrame { get; set; }
        private int _frameCounter;
        public int FrameCounter 
        {
            get { return _frameCounter; }
            set
            {
                if (value <= _currentAnimation?.AnimationFrames.Count - 1)
                {
                    _frameCounter = value;
                }
                else _frameCounter = 0;
            }
        }
        private string animationName;
        public string AnimationName
        {
            get
            {
                return animationName;
            }
            set
            {
                if(animationName != value)
                {
                    animationName = value;
                    UpdateAnimation();
                }
            }
        }
        private float _animationDuration { get; set; }
        private float _animationTimePassed { get; set; }
        public float AnimationDuration { get { return _animationDuration; } }
        public float AnimationTimeRemaining { get { return _animationDuration - _animationTimePassed; } }
        private Animation _currentAnimation { get; set; }

        public Dictionary<string, Animation> Animations { get; set; }
        public Sprite AnimationSprite { get; set; }

        public Animated(AnimationProvider animationProvider, MapProvider mapProvider)
        {
            Animations = new();
            _animationProvider = animationProvider;
            _mapProvider = mapProvider;
        }
        private void ResetTimers()
        {
            _animationTimePassed = 0;
            if(Timer is not null)
                Timer.Restart();
        }
        private void UpdateAnimation()
        {
            if (Animations.TryGetValue(AnimationName,  out Animation value))
            {
                _currentAnimation = value;
                FrameCounter = 0;
                float total = 0;
                foreach(AnimationFrame frame in value.AnimationFrames)
                {
                    total += frame.Duration;
                }
                _animationDuration = total;
                _animationTimePassed = 0;
            }
            else throw new Exception($"Player does not contain an Animation with name {AnimationName}.");
        }
        private void SetSprite()
        {
            Sprite newSprite = new Sprite();
            CurrentFrame = _currentAnimation.AnimationFrames.ElementAt(FrameCounter++);
            //switch currentframe to next frame

            var frameTile = _mapProvider.GetTileSetTile(TileSetName, CurrentFrame.TileId);
            AnimationSprite.Texture = frameTile.ImageTexture;
        }

        public virtual void Init()
        {
            Timer = Stopwatch.StartNew();
            FrameCounter = 0;
            if (Animations.TryGetValue(AnimationName, out Animation value))
            {
                _currentAnimation = value;
                CurrentFrame = _currentAnimation.AnimationFrames.ElementAt(FrameCounter);
                AnimationSprite = new();
                SetSprite();
            }
            else throw new Exception($"Player does not contain an Animation with name {AnimationName}.");
        }
        
        public void Tick()
        {
            if (Timer.ElapsedMilliseconds > CurrentFrame.Duration)
            {
                _animationTimePassed += Timer.ElapsedMilliseconds;
                Timer.Restart();
                AnimationTick();
            }
        }
        private void AnimationTick()
        {

            if (Animations.TryGetValue(AnimationName, out Animation value))
            {
                _currentAnimation = value;
                SetSprite();
            }
            else throw new Exception($"Player does not contain an Animation with name {AnimationName}.");
        }
    }
}
