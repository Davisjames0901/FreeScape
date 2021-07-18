using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using FreeScape.Engine.Config;
using FreeScape.Engine.Config.Map;
using FreeScape.Engine.Config.TileSet;
using FreeScape.Engine.GameObjects.Entities;
using FreeScape.Engine.Utilities;
using SFML.Graphics;

namespace FreeScape.Engine.Providers
{
    public class MapProvider
    {
        private readonly JsonSerializerOptions _options;
        private readonly Dictionary<string, MapInfo> _maps;
        private readonly Dictionary<string, CachedTileSet> _tileSets;
        private readonly GameInfo _gameInfo;
        private readonly AnimationProvider _animationProvider;

        public string CurrentMapName { get; set; }

        public MapProvider(GameInfo gameInfo, JsonSerializerOptions options, AnimationProvider animationProvider)
        {
            _options = options;
            _gameInfo = gameInfo;
            _animationProvider = animationProvider;
            _maps = new Dictionary<string, MapInfo>();
            _tileSets = new Dictionary<string, CachedTileSet>();
            foreach (var file in Directory.EnumerateFiles(gameInfo.MapDirectory)
                .Where(x => x.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)))
            {
                var name = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
                _maps.Add(name, ReadMapFile(file));
            }
            foreach (var map in _maps)
            {
                var mapName = map.Key;
                var mapInfo = map.Value;
                foreach(var tileSet in mapInfo.TileSets)
                {
                    var name = $"{mapName}:{tileSet.Name}";

                    CacheTileSet(name, tileSet);

                    //CacheTilesFromTileSet(tileSet);
                }
            }
        }
        private void CacheTileSet(string name, TileSet tileSet)
        {
            CachedTileSet cachedTileSet = new();
            if (tileSet.Image != null)
            {
                Texture tileSheet = new Texture($"{_gameInfo.TileSetDirectory}{Path.DirectorySeparatorChar}{tileSet.Image}");
                cachedTileSet.Sheet = tileSheet;
            }
            cachedTileSet.Tiles = CacheTilesFromTileSet(tileSet);
            cachedTileSet.Name = tileSet.Name;
            cachedTileSet.Columns = tileSet.Columns;
            cachedTileSet.FirstGid = tileSet.FirstGid;
            cachedTileSet.Image = tileSet.Image;
            cachedTileSet.ImageHeight = tileSet.ImageHeight;
            cachedTileSet.ImageWidth = tileSet.ImageWidth;
            cachedTileSet.Spacing = tileSet.Spacing;
            cachedTileSet.TileCount = tileSet.TileCount;
            cachedTileSet.TileHeight = tileSet.TileHeight;
            cachedTileSet.TileWidth = tileSet.TileWidth;


            _tileSets.Add(name, cachedTileSet);
        }
        private Dictionary<uint, CachedTileSetTile> CacheTilesFromTileSet(TileSet tileSet)
        {
            Dictionary<uint, CachedTileSetTile> cachedTileSetTiles = new Dictionary<uint, CachedTileSetTile>(); ;
            var i = 0;
            foreach(var tile in tileSet.Tiles)
            {
                var cachedTileSetTile = new CachedTileSetTile();

                if (tileSet.Image != null)
                {
                    var imageWidth = tileSet.Columns * tileSet.TileWidth;
                    var tileSize = new Vector2(tileSet.TileWidth, tileSet.TileWidth);
                    var imageSize = new Vector2(tileSet.ImageWidth, tileSet.ImageHeight);
                    imageSize = Maths.Floor(imageSize / tileSize) * tileSize;
                    var tileLocation = new Vector2(tile.Id % tileSet.Columns, (int)Math.Floor((float)tile.Id / tileSet.Columns)) * tileSize;
                    IntRect textureLocation = new IntRect(tileLocation,
                                                        new Vector2(tileSet.TileWidth, tileSet.TileHeight));
                    cachedTileSetTile.UsesSheet = true;
                    cachedTileSetTile.TextureLocation = textureLocation;
                }
                else
                {
                    cachedTileSetTile.UsesSheet = false;
                    var newTexture = new Texture($"{ _gameInfo.TextureDirectory }{ Path.DirectorySeparatorChar}{ tile.Image}");
                    cachedTileSetTile.ImageHeight = tile.ImageHeight;
                    cachedTileSetTile.ImageWidth = tile.ImageWidth;
                    cachedTileSetTile.ImageTexture = newTexture;
                }

                cachedTileSetTile.Animation = tile.Animation;
                cachedTileSetTile.Id = tile.Id;
                cachedTileSetTile.Properties = tile.Properties;
                cachedTileSetTile.ObjectGroup = tile.ObjectGroup;
                cachedTileSetTile.Type = tile.Type;
                cachedTileSetTile.TileSetName = tileSet.Name;
                cachedTileSetTiles.Add(tile.Id, cachedTileSetTile);
                if (cachedTileSetTile.Animation is not null)
                {
                    _animationProvider.CreateAndAddAnimation(cachedTileSetTile);
                }
                i++;
            }
            return cachedTileSetTiles;
        }
        public CachedTileSet GetTileSet(string tileSetName)
        {
            if (_tileSets.TryGetValue(CurrentMapName + ":" + tileSetName, out CachedTileSet tileSet))
            {
                return tileSet;
            }
            else
                throw new Exception($"TileSet does not exist with name {tileSetName}");
        }
        public CachedTileSet GetTileSetBy(int tileId)
        {

            foreach (var tileSet in _tileSets.OrderBy(x => x.Value.FirstGid))
            {
                //Check the ID of the tile vs the tileSet's largest ID. If our tile ID is larger, it's not in this tileset
                if (tileId > tileSet.Value.FirstGid + tileSet.Value.Tiles.OrderByDescending(x => x.Value.Id).FirstOrDefault().Value.Id)
                    continue;

                return tileSet.Value;
            }

            return null;
        }
        public CachedTileSetTile GetTileSetTile(string tileSetName, int tileId)
        {
            var tileSet = GetTileSet(tileSetName);
            if (tileSet.Tiles.TryGetValue((uint)tileId, out CachedTileSetTile tile))
            {
                return tile;
            }

            return null;
        }
        public CachedTileSetTile GetTileSetTile(CachedTileSet tileSet, int tileId)
        {
            if (tileSet.Tiles.TryGetValue((uint)tileId, out CachedTileSetTile tile))
            {
                return tile;
            }

            return null;
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

        private MapInfo ReadMapFile(string path)
        {
            var text = File.ReadAllText(path);
            
            return JsonSerializer.Deserialize<MapInfo>(text, _options);
        }
    }
}