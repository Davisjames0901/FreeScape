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

        public MapProvider(GameInfo gameInfo, JsonSerializerOptions options)
        {
            _options = options;
            _gameInfo = gameInfo;
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
                var tileSetTile = new CachedTileSetTile();

                if (tileSet.Image != null)
                {
                    var imageWidth = tileSet.Columns * tileSet.TileWidth;
                    var tileSize = new Vector2(tileSet.TileWidth, tileSet.TileWidth);
                    var imageSize = new Vector2(tileSet.ImageWidth, tileSet.ImageHeight);
                    imageSize = Maths.Floor(imageSize / tileSize) * tileSize;
                    if(i == 44)
                    {
                        Console.WriteLine();
                    }
                    var tileLocation = new Vector2(tile.Id % tileSet.Columns, (int)Math.Floor((float)tile.Id / tileSet.Columns)) * tileSize;
                    IntRect textureLocation = new IntRect(tileLocation,
                                                        new Vector2(tileSet.TileWidth, tileSet.TileHeight));
                    tileSetTile.TextureLocation = textureLocation;
                }
                else
                {

                    string texturePath = tile.Image;
                }

                tileSetTile.Animation = tile.Animation;
                tileSetTile.Id = tile.Id;
                tileSetTile.Properties = tile.Properties;
                tileSetTile.ObjectGroup = tile.ObjectGroup;
                tileSetTile.Type = tile.Type;
                tileSetTile.TileSetName = tileSet.Name;
                cachedTileSetTiles.Add(tile.Id, tileSetTile);
                i++;
            }
            return cachedTileSetTiles;
        }
        public CachedTileSet GetTileSetByTileId(int id)
        {

            foreach (var tileSet in _tileSets.OrderBy(x => x.Value.FirstGid))
            {
                //Check the ID of the tile vs the tileSet's largest ID. If our tile ID is larger, it's not in this tileset
                if (id > tileSet.Value.FirstGid + tileSet.Value.Tiles.OrderByDescending(x => x.Value.Id).FirstOrDefault().Value.Id)
                    continue;

                return tileSet.Value;
            }

            return null;
        }
        public CachedTileSetTile GetTileSetTileById(int id)
        {
            //TODO send the map name to speed things up
            //Check each tileset in an ascending order based on the FirstGid property.
            foreach (var tileSet in _tileSets.OrderBy(x => x.Value.FirstGid))
            {
                //Check the ID of the tile vs the tileSet's largest ID. If our tile ID is larger, it's not in this tileset
                if (id > tileSet.Value.FirstGid + tileSet.Value.Tiles.OrderByDescending(x => x.Value.Id).FirstOrDefault().Value.Id)
                    continue;
                if (tileSet.Value.Tiles.TryGetValue((uint)id, out CachedTileSetTile tile))
                {
                    return tile;
                }
            }

            return null;
        }

        public MapInfo GetMap(string mapName)
        {
            if (_maps.ContainsKey(mapName))
                return _maps[mapName];
            return null;
        }

        private MapInfo ReadMapFile(string path)
        {
            var text = File.ReadAllText(path);
            
            return JsonSerializer.Deserialize<MapInfo>(text, _options);
        }
    }
}