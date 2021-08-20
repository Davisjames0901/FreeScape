using System;
using System.Collections.Generic;
using System.Linq;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Movement;
using FreeScape.Engine.Render.Animations.AnimationTypes;
using FreeScape.Engine.Render.Textures;
using FreeScape.Engine.Render.Tiled;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Render.Animations
{
    public class AnimationProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TextureProvider _textureProvider;
        private readonly Dictionary<string, List<TiledAnimationFrame>> _animations;

        public AnimationProvider(IServiceProvider serviceProvider, TextureProvider textureProvider)
        {
            _serviceProvider = serviceProvider;
            _textureProvider = textureProvider;
            _animations = new();
        }

        public void CreateAndAddAnimation(List<TiledAnimationFrame> frames, int gid, string name)
        {
            foreach (var frame in frames)
            {
                frame.TileId += gid;
            }
            Console.WriteLine($"Name: {name}, Frames: {string.Join(' ', frames.Select(x=> x.TileId))}");
            _animations.Add(name, frames);
        }

        public T GetAnimation<T>(string name) where T : IAnimation, ISingleAnimation
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

        /// <summary>
        /// Gets a group of ISingleAnimations for each movement direction and returns as an IAnimation
        /// </summary>
        /// <param name="animationName">Base animation name, variations will be applied to this like :up, :down etc...</param>
        /// <param name="movable">This is so we can capture direction information</param>
        /// <typeparam name="T">The style of animation you want to have for all the directions</typeparam>
        /// <returns>Aggregate animation</returns>
        public IAnimation GetDirectionAnimation<T>(string animationName, IMovable movable) where T : ISingleAnimation
        {
            var animation = new DirectionAnimation() as ISwitchedGroupAnimation<Direction>;
            List<(IAnimation animation, Direction direction)> animations = new();
            animations.Add((GetAnimation<T>(animationName + ":up"), Direction.Up));
            animations.Add((GetAnimation<T>(animationName + ":down"), Direction.Down));
            animations.Add((GetAnimation<T>(animationName + ":left"), Direction.Left));
            animations.Add((GetAnimation<T>(animationName + ":right"), Direction.Right));
            
            //The selector func should capture allocate the IMovable here
            animation.LoadAnimations(animations, () => movable.HeadingVector.GetLastDirection());
            
            return animation;
        }
    }
}
