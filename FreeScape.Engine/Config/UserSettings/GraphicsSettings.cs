using System.Numerics;

namespace FreeScape.Engine.Config.UserSettings
{
    public class GraphicsSettings : BaseSetting
    {
        private uint _screenWidth;

        public uint ScreenWidth
        {
            get => _screenWidth;
            set
            {
                _screenWidth = value;
                Changed();
            }
        }

        private uint _screenHeight;

        public uint ScreenHeight
        {
            get => _screenHeight;
            set
            {
                _screenHeight = value;
                Changed();
            }
        }

        private bool _vSyncEnabled;
        private uint _refreshRate;

        public bool VSyncEnabled
        {
            get => _vSyncEnabled;
            set
            {
                _vSyncEnabled = value;
                Changed();
            }
        }

        public uint RefreshRate
        {
            get => _refreshRate;
            set
            {
                _refreshRate = value;
                Changed();
            }
                
        }

        public Vector2 ScreenSize => new(ScreenWidth, ScreenHeight);

        public override void Dispose()
        {
        }
    }
}