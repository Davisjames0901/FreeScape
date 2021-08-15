using FreeScape.Engine.Config.TileSet;
using System;
using System.Collections.Generic;
using System.Linq;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Movements;
using FreeScape.Engine.Render.Animations;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Providers
{
    public class AnimationProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TextureProvider _textureProvider;
        private readonly Dictionary<string, List<AnimationFrameInfo>> _animations;

        public AnimationProvider(IServiceProvider serviceProvider, TextureProvider textureProvider)
        {
            _serviceProvider = serviceProvider;
            _textureProvider = textureProvider;
            _animations = new();
        }

        public void CreateAndAddAnimation(List<AnimationFrameInfo> frames, int gid, string name)
        {
            foreach (var frame in frames)
            {
                frame.TileId += gid;
            }
            Console.WriteLine($"Name: {name}, Frames: {string.Join(' ', frames.Select(x=> x.TileId))}");
            _animations.Add(name, frames);
        }

        public T GetAnimation<T>(string name) where T : IAnimation
        {
            var animation = _serviceProvider.GetService<T>();
            if (animation == null)
                throw new Exception($"There is no IAnimation impl registered for type {typeof(T)}");
            if (!_animations.ContainsKey(name))
                throw new Exception($"There is no animation with the name {name}");
            
            var animationInfo = _animations[name];
            var frames = new List<AnimationFrame>();
            foreach (var frameInfo in animationInfo)
            {
                var sprite = _textureProvider.GetSprite($"tiled:{frameInfo.TileId}");
                frames.Add(new AnimationFrame
                {
                    Sprite = sprite,
                    Duration = frameInfo.Duration
                });
            }
            animation.LoadFrames(frames);
            return animation;
        }

        public DirectionAnimation GetDirectionAnimation<T>(string animationName, IMovable moveable) where T : IAnimation
        {
            var animation = new DirectionAnimation();
            List<(IAnimation animation, Direction direction)> animations = new();
            animations.Add((GetAnimation<T>(animationName + ":up"), Direction.Up));
            animations.Add((GetAnimation<T>(animationName + ":down"), Direction.Down));
            animations.Add((GetAnimation<T>(animationName + ":left"), Direction.Left));
            animations.Add((GetAnimation<T>(animationName + ":right"), Direction.Right));
            
            animation.LoadAnimations(animations, moveable);
            
            return animation;
        }
        // public List<Animation> GetMovementAnimations(string name)
        // {
        //     List<Animation> animations = new();
        //     animations.Add(GetAnimation(name + ":up"));
        //     animations.Add(GetAnimation(name + ":down"));
        //     animations.Add(GetAnimation(name + ":left"));
        //     animations.Add(GetAnimation(name + ":right"));
        //
        //     return animations;
        // }
    }
}
