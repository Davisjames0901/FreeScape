using System.IO;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Principal;
using SFML.System;

namespace FreeScape.Engine.Config
{
    public class GameInfo
    {
        public string Name { get; init; }
        public string AssetDirectory { get; init; }
        public string TextureDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}Textures";
        public string SettingsDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}Settings";
        public string TileSetDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}Textures{Path.DirectorySeparatorChar}TileSets";
        public string MapDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}Maps";
        public string ActionMapDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}ActionMaps";
        public string SoundDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}Sounds";
    }
}