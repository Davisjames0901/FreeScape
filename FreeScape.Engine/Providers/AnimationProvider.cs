using FreeScape.Engine.Config.TileSet;
using FreeScape.Engine.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.Providers
{
    public class AnimationProvider
    {
        private readonly Dictionary<string, Animation> _animations;

        public AnimationProvider()
        {
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

        public Animation GetAnimation(string name)
        {
            if (_animations.TryGetValue(name, out Animation value))
            {
                return value;
            }
            else
                throw new Exception($"An Animation does not exist for {name}.");
        }
        public List<Animation> GetMovementAnimation(string name)
        {
            List<Animation> animations = new();
            animations.Add(GetAnimation(name + ":up"));
            animations.Add(GetAnimation(name + ":down"));
            animations.Add(GetAnimation(name + ":left"));
            animations.Add(GetAnimation(name + ":right"));

            return animations;
        }
    }
}
