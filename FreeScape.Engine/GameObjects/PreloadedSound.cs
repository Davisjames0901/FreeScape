using SFML.Audio;

namespace FreeScape.Engine.GameObjects
{
    public class PreloadedSound
    {
        public PreloadedSound(string filePath, GameInfo info)
        {
            Buffer = new SoundBuffer(filePath);
            Sound = new Sound(Buffer);
        }
        public SoundBuffer Buffer { get; set; }
        public Sound Sound { get; set; }
    }
}