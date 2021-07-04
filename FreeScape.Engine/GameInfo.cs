using System.IO;

namespace FreeScape.Engine
{
    public class GameInfo
    {
        public string Name { get; init; }
        public uint ScreenWidth { get; init; }
        public uint ScreenHeight { get; init; }
        public uint RefreshRate { get; init; }
        public string AssetDirectory { get; init; }
        public string TextureDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}Textures";
        public string MapDirectory => $"{AssetDirectory}{Path.DirectorySeparatorChar}Maps";
    }
}