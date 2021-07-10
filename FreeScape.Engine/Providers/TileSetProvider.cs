using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Xml.Schema;
using FreeScape.Engine.Config;
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
            foreach (var item in info.Tiles)
            {
                var tileLocationX = (((item.Id-1) * info.TileWidth) % info.ImageWidth);
                var tileLocationY = ((item.Id-1) * info.TileHeight / info.ImageHeight);
                tileSet.Tiles.Add(item.Id, new CachedTileSetTile
                {
                    Id = item.Id,
                    Properties = item.Properties,
                    TextureLocation = new IntRect(new Vector2(tileLocationX, tileLocationY), tileSize)
                });
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