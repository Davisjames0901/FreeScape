using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Render;
using SFML.Audio;
using SFML.Graphics;

namespace FreeScape.Engine.Providers
{
    public class SoundProvider
    {
        private readonly GameInfo _info;
        private readonly Dictionary<string, SoundInfo> _soundDescriptors;
        private readonly Dictionary<string, Sound> _preloadSounds;
        
        public SoundProvider(GameInfo info)
        {
            _info = info;
            _soundDescriptors = new Dictionary<string, SoundInfo>();
            _preloadSounds = new Dictionary<string, Sound>();
            
            foreach (var file in Directory.EnumerateFiles(info.SoundDirectory).Where(x => x.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)))
            {
                var name = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
                var descriptor = GetDescriptor(file);
                if(descriptor.Preload)
                    
                _soundDescriptors.Add(name, descriptor);
            }
        }

        private SoundInfo GetDescriptor(string path)
        {
            var text = File.ReadAllText(path);
            var descriptor = JsonSerializer.Deserialize<SoundInfo>(text);
            descriptor.FilePath = path;
            return descriptor;
        }

        private void PreloadSound(SoundInfo info, string name)
        {
            
        }
    }
}