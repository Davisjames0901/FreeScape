using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using FreeScape.Engine.Config;
using FreeScape.Engine.Config.Map;
using FreeScape.Engine.Config.TileSet;
using SFML.Graphics;

namespace FreeScape.Engine.Providers
{
    public class MapProvider
    {
        private readonly JsonSerializerOptions _options;
        private readonly Dictionary<string, MapInfo> _maps;
        private readonly Dictionary<uint, TileSetTile> _tiles;
        private readonly AnimationProvider _animationProvider;
        private readonly TextureProvider _textureProvider;

        public string CurrentMapName { get; set; }

        public MapProvider(GameInfo gameInfo, JsonSerializerOptions options, AnimationProvider animationProvider, TextureProvider textureProvider)
        {
            _options = options;
            _animationProvider = animationProvider;
            _textureProvider = textureProvider;
            _maps = new Dictionary<string, MapInfo>();
            _tiles = new Dictionary<uint, TileSetTile>();
            foreach (var file in Directory.EnumerateFiles(gameInfo.MapDirectory)
                .Where(x => x.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)))
            {
                var name = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
                _maps.Add(name, ReadMapFile(file));
            }
        }
        
        private void ProcessAndAddTiles(TileSet tileSet)
        {
            foreach(var tile in tileSet.Tiles)
            {
                TextureInfo textureInfo; 
                var correctedId = tile.Id + (uint)tileSet.FirstGid;
                if (tileSet.Image != null)
                    textureInfo = GetTextureInfoFromTileSet(tile, tileSet, correctedId);
                else
                    textureInfo = GetTextureInfo(tile, correctedId);

                if (tile.Animation is not null)
                    _animationProvider.CreateAndAddAnimation(tile.Animation, tileSet.FirstGid, tile.Type);

                tile.Id = correctedId;
                _textureProvider.CreateAndAddTexture($"tiled:{textureInfo.Name}", textureInfo);
                _tiles.Add(correctedId, tile);
            }
        }
        public TileSetTile GetTile(uint gid)
        {
            if (_tiles.TryGetValue(gid, out var tile))
                return tile;
            
            throw new Exception($"Tile does not exist with GID {gid}");
        }

        public MapInfo GetMap(string mapName)
        {
            if (_maps.ContainsKey(mapName))
            {
                CurrentMapName = mapName;
                return _maps[mapName];
            }
            return null;
        }

        private TextureInfo GetTextureInfoFromTileSet(TileSetTile tile, TileSet tileSet, uint correctedId)
        {
            var tileSize = new Vector2(tileSet.TileWidth, tileSet.TileWidth);
            var tileLocation = new Vector2(tile.Id % tileSet.Columns, (int)Math.Floor((float)tile.Id / tileSet.Columns)) * tileSize;
            var textureLocation = new IntRect(tileLocation, new Vector2(tileSet.TileWidth, tileSet.TileHeight));
            return new TextureInfo
            {
                File = $"TileSets{Path.DirectorySeparatorChar}{tileSet.Image}",
                X = textureLocation.Left,
                Y = textureLocation.Top,
                Height = textureLocation.Height,
                Width = textureLocation.Width,
                Name = correctedId.ToString(),
                Smooth = false
            };
        }

        private TextureInfo GetTextureInfo(TileSetTile tile, uint correctedId)
        {
            return new TextureInfo
            {
                File = tile.Image,
                Height = (int)tile.ImageHeight,
                Width = (int)tile.ImageWidth,
                X = 0,
                Y = 0,
                Name = correctedId.ToString(),
                Smooth = true
            };
        }

        private MapInfo ReadMapFile(string path)
        {
            var text = File.ReadAllText(path);
            var map = JsonSerializer.Deserialize<MapInfo>(text, _options);
            foreach (var tileSet in map.TileSets)
            {
                ProcessAndAddTiles(tileSet);
            }

            return map;
        }
    }
}