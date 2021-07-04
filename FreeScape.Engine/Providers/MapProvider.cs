using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FreeScape.Engine.Render;

namespace FreeScape.Engine.Providers
{
    public class MapProvider
    {
        private readonly Dictionary<string, MapInfo> _maps;

        public MapProvider(GameInfo gameInfo)
        {
            _maps = new Dictionary<string, MapInfo>();
            foreach (var file in Directory.EnumerateFiles(gameInfo.MapDirectory)
                .Where(x => x.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)))
            {
                var name = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
                _maps.Add(name, ReadMapFile(file));
            }
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
            return JsonSerializer.Deserialize<MapInfo>(text);
        }
        
    }
}