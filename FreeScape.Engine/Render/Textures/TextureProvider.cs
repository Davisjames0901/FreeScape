using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FreeScape.Engine.Settings;
using SFML.Graphics;

namespace FreeScape.Engine.Render.Textures
{
    public class TextureProvider
    {
        private readonly GameInfo _info;
        private readonly Dictionary<string, List<TextureInfo>> _textureDescriptors;
        private readonly Dictionary<string, Texture> _textureCache;
        private readonly Dictionary<string, Func<Sprite>> _spriteSelectors;
        
        public TextureProvider(GameInfo info)
        {
            _info = info;
            _textureDescriptors = new Dictionary<string, List<TextureInfo>>();
            _spriteSelectors = new Dictionary<string, Func<Sprite>>();
            _textureCache = new Dictionary<string, Texture>();
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

        internal void CreateAndAddTexture(string gtl, TextureInfo descriptor)
        {
            var path = $"{_info.TextureDirectory}{Path.DirectorySeparatorChar}{descriptor.File}";
            if (!File.Exists(path))
                return;

            if(!_textureCache.ContainsKey(path))
                _textureCache.Add(path, new Texture(path));
            
            Console.WriteLine($"Gtl: {gtl}, {descriptor.File}");
            _spriteSelectors.Add(gtl, () => new Sprite(_textureCache[path], descriptor.Location));
        }

        //We should try to get rid of this and use the texture descriptors for loading instead
        public Texture GetTextureByFile(string filePath, string gtl)
        {
            var path = $"{_info.TextureDirectory}/{filePath}.png";
        
            if (!File.Exists(path))
                return null;

            return new Texture(path);
        }
        
        public Sprite GetSprite(string gtl)
        {
            var tokens = gtl.Split(':');
            return GetSprite(tokens[0], tokens[1]);
        }
        
        public Sprite GetSprite(string sheetName, string textureName)
        {
            var gtl = $"{sheetName}:{textureName}";
            
            if (!_spriteSelectors.ContainsKey(gtl))
            {
                if (!_textureDescriptors.ContainsKey(sheetName))
                    return null;
                var sheet = _textureDescriptors[sheetName];
                var texture = sheet.FirstOrDefault(x => x.Name == textureName);
                if (texture == null)
                    return null;
                CreateAndAddTexture(gtl, texture);
            }

            return _spriteSelectors[gtl]();
        }
    }
}