using FreeScape.Engine.Config.Sound;
using SFML.Audio;

namespace FreeScape.Engine.GameObjects
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