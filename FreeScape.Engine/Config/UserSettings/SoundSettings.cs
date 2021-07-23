namespace FreeScape.Engine.Config.UserSettings
{
    public class SoundSettings: BaseSetting
    {
        private float _sfxVolume;
        private float _musicVolume;

        public float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = value;
                Changed();
            }
        }

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                Changed();
            }
        }

        public override void Dispose()
        {
            
        }
    }
}