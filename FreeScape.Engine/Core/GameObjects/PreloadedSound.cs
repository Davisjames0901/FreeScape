using FreeScape.Engine.Sfx;
using SFML.Audio;

namespace FreeScape.Engine.Core.GameObjects
{
    public class PreloadedSound
    {
        public PreloadedSound(SoundInfo info)
        {
            Buffer = new SoundBuffer(info.FilePath);
            Sound = new Sound(Buffer);
            SoundInfo = info;
        }
        public SoundBuffer Buffer { get; }
        public Sound Sound { get; }
        public SoundInfo SoundInfo { get; }
    }
}