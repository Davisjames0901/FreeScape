using System.Collections.Generic;
using FreeScape.Engine.Config.Map;
using SFML.Graphics;

namespace FreeScape.Engine.Config.TileSet
{
    public class CachedTileSetTile
    {
        public IntRect TextureLocation { get; set; }
        public string FilePath { get; set; }
        public string TileSetName { get; set; }
        public uint Id { get; set; }
        public List<MapProperties> Properties { get; set; }
        public TileObjectGroup ObjectGroup { get; set; }
        public string Type { get; set; }

        public List<AnimationFrameInfo> Animation;
        public float ImageWidth { get; set; }
        public float ImageHeight { get; set; }
        public Texture ImageTexture { get; set; }
        public bool UsesSheet { get; set; }
        public RectangleShape RectangleShape { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}; Location: {TextureLocation}";
        }
    }
}