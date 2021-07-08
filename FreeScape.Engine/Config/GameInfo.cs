using System.IO;
using System.Numerics;
using SFML.System;

namespace FreeScape.Engine.Config
{
    public class GameInfo
    {
        public string Name { get; init; }
        public uint ScreenWidth { get; init; }
        public uint ScreenHeight { get; init; }
        public Vector2 ScreenSize => new (ScreenWidth, ScreenHeight);
        public uint RefreshRate { get; init; }
        public string AssetDirectory { get; init; }
        public float SfxVolume { get; set; }
        public float MusicVolume { get; set; }
        public string TextureDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}Textures";
        public string MapDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}Maps";
        public string ActionMapDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}ActionMaps";
        public string SoundDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}Sounds";
    }
}