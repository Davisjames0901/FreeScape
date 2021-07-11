using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FreeScape.Engine.Config;
using FreeScape.Engine.Config.Sound;
using FreeScape.Engine.GameObjects;
using SFML.Audio;

namespace FreeScape.Engine.Providers
{
    public class SoundProvider
    {
        private readonly GameInfo _info;
        private readonly Dictionary<string, SoundInfo> _soundDescriptors;
        private readonly Dictionary<string, PreloadedSound> _preloadSounds;
        private Music _currentTrack;
        
        public SoundProvider(GameInfo info)
        {
            _info = info;
            _soundDescriptors = new Dictionary<string, SoundInfo>();
            _preloadSounds = new Dictionary<string, PreloadedSound>();
            
            foreach (var file in Directory.EnumerateFiles(info.SoundDirectory).Where(x => x.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)))
            {
                var name = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
                var descriptor = GetDescriptor(file, name);
                if(descriptor.Preload)
                    PreloadSound(descriptor, name);
                _soundDescriptors.Add(name, descriptor);
            }
        }

        public void PlaySound(string name)
        {
            if (_preloadSounds.TryGetValue(name, out var sound))
            {
                sound.Sound.Volume = _info.SfxVolume;
                sound.Sound.Play();
            }
        }

        public void PlayMusic(string name)
        {
            if (_soundDescriptors.TryGetValue(name, out var soundInfo))
            {
                _currentTrack?.Stop();
                _currentTrack?.Dispose();
                _currentTrack = new Music(soundInfo.FilePath);
                _currentTrack.Volume = _info.MusicVolume;
                _currentTrack.Loop = soundInfo.IsLooped;
                _currentTrack.Play();
            }
        }

        private SoundInfo GetDescriptor(string path, string name)
        {
            var text = File.ReadAllText(path);
            var descriptor = JsonSerializer.Deserialize<SoundInfo>(text);
            descriptor.FilePath = $"{_info.SoundDirectory}{Path.DirectorySeparatorChar}{name}{descriptor.Extension}";
            return descriptor;
        }

        private void PreloadSound(SoundInfo info, string name)
        {
            var sound = new PreloadedSound(info);
            _preloadSounds.Add(name, sound);
        }
        
        
    }
}