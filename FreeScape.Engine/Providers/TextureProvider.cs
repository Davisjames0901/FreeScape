using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FreeScape.Engine.Config;
using SFML.Graphics;

namespace FreeScape.Engine.Providers
{
    public class TextureProvider
    {
        private readonly GameInfo _info;
        private readonly Dictionary<string, List<TextureInfo>> _textureDescriptors;
        private readonly Dictionary<string, Texture> _textures;
        public TextureProvider(GameInfo info)
        {
            _info = info;
            _textureDescriptors = new Dictionary<string, List<TextureInfo>>();
            _textures = new Dictionary<string, Texture>();
            foreach (var file in Directory.EnumerateFiles(info.TextureDirectory).Where(x => x.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)))
            {
                var name = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
                _textureDescriptors.Add(name, GetDescriptors(file));
            }
        }

        private List<TextureInfo> GetDescriptors(string path)
        {
            var text = File.ReadAllText(path);
            var descriptor = JsonSerializer.Deserialize<List<TextureInfo>>(text);
            descriptor?.ForEach(x=> x.File = path);
            return descriptor;
        }

        private Texture CreateAndAddTexture(string textureName, string gtl, TextureInfo descriptor)
        {
            var path = $"{_info.TextureDirectory}/{textureName}.png";
            if (!File.Exists(path))
                return null;

            var texture = new Texture(path, new IntRect(descriptor.X, descriptor.Y, descriptor.Width, descriptor.Height));
            _textures.Add(gtl, texture);

            return texture;
        }

        public Texture? GetTextureByFile(string filePath, string gtl)
        {

            var path = $"{_info.TextureDirectory}/{filePath}.png";

            if (!File.Exists(path))
                return null;

            if (_textures.ContainsKey(gtl))
                return _textures[gtl];

            var texture = new Texture(path);
            _textures.Add(gtl, texture);

            return texture;
        }
        public Texture? GetTexture(string gtl)
        {
            var tokens = gtl.Split(':');
            return GetTexture(tokens[0], tokens[1]);
        }
        
        public Texture? GetTexture(string sheetName, string textureName)
        {
            var gtl = $"{sheetName}:{textureName}";
            if (_textures.ContainsKey(gtl))
                return _textures[gtl];

            if (!_textureDescriptors.ContainsKey(sheetName))
                return null;

            var sheet = _textureDescriptors[sheetName];
            var texture = sheet.FirstOrDefault(x => x.Name == textureName);
            if (texture == null)
                return null;

            return CreateAndAddTexture(sheetName, gtl, texture);
        }
    }
}