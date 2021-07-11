using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Xml.Schema;
using FreeScape.Engine.Config;
using FreeScape.Engine.Config.TileSet;
using FreeScape.Engine.Utilities;
using SFML.Graphics;

namespace FreeScape.Engine.Providers
{
    public class TileSetProvider
    {
        private readonly GameInfo _info;
        private readonly JsonSerializerOptions _options;
        private readonly List<CachedTileSet> _tileSets;
        public TileSetProvider(GameInfo info, JsonSerializerOptions options)
        {
            _info = info;
            _options = options;
            _tileSets = new List<CachedTileSet>();
            foreach (var file in Directory.EnumerateFiles(info.TileSetDirectory).Where(x => x.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)))
            {
                var name = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
                var descriptor = GetDescriptor(file);
                CacheTileSet(descriptor);
            }
        }

        private void CacheTileSet(TileSetInfo info)
        {
            var tileSet = new CachedTileSet();
            var sheetPath = $"{_info.TileSetDirectory}{Path.DirectorySeparatorChar}{info.Image}";
            tileSet.Sheet = new Texture(sheetPath);
            tileSet.Tiles = new Dictionary<uint, CachedTileSetTile>();
            tileSet.Name = info.Name;
            var tileSize = new Vector2(info.TileWidth, info.TileWidth);
            var imageSize = new Vector2(info.ImageWidth, info.ImageHeight);
            imageSize = Maths.Floor(imageSize / tileSize) * tileSize;
            foreach (var item in info.Tiles)
            {
                var tiles = imageSize / tileSize;
                var tileLocation = new Vector2(item.Id % tiles.X, (int)Math.Floor(item.Id/tiles.X)) * tileSize;
                
                var tile = new CachedTileSetTile
                {
                    Id = item.Id,
                    Properties = item.Properties,
                    TextureLocation = new IntRect(tileLocation, tileSize)
                };
                tileSet.Tiles.Add(item.Id, tile);
            }
            _tileSets.Add(tileSet);
        }

        private TileSetInfo GetDescriptor(string path)
        {
            var text = File.ReadAllText(path);
            return JsonSerializer.Deserialize<TileSetInfo>(text, _options);
        }

        public CachedTileSet GetTileSet(string name)
        {
            return _tileSets.FirstOrDefault(x => x.Name == name);
        }
    }
}