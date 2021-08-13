using FreeScape.Engine.Config.TileSet;
using System;
using System.Collections.Generic;
using FreeScape.Engine.Render.Animations;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Providers
{
    public class AnimationProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TextureProvider _textureProvider;
        private readonly Dictionary<string, Animation> _animations;

        public AnimationProvider(IServiceProvider serviceProvider, TextureProvider textureProvider)
        {
            _serviceProvider = serviceProvider;
            _textureProvider = textureProvider;
            _animations = new();
        }

        public void CreateAndAddAnimation(CachedTileSetTile tileInfo)
        {
            Animation animation = new Animation();
            animation.ImageHeight = tileInfo.ImageHeight;
            animation.ImageWidth = tileInfo.ImageWidth;
            animation.AnimationFrames = tileInfo.Animation;
            animation.Id = tileInfo.Id;
            animation.Type = tileInfo.Type;
            _animations.Add(animation.Type, animation);
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
            foreach (var frameInfo in animationInfo.AnimationFrames)
            {
                var texture = _textureProvider.GetTexture($"animation:{frameInfo.TileId}");
                frames.Add(new AnimationFrame
                {
                    Texture = texture,
                    Duration = frameInfo.Duration
                });
            }
            animation.LoadFrames(frames);
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
