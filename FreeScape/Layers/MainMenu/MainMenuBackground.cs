using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using FreeScape.Engine.Render.Layers;
using FreeScape.Engine.Utilities;
using SFML.Graphics;

namespace FreeScape.Layers.MainMenu
{
    public class MainMenuBackground : ILayer
    {
        private readonly DisplayManager _displayManager;
        private readonly Queue<Vector2> _backgroundTrack;
        private readonly Sprite _background;
        private Vector2 _currentBackgroundTarget;
        
        public RenderMode RenderMode => RenderMode.World;
        public int ZIndex => 0;
        public MainMenuBackground(TextureProvider textureProvider, DisplayManager displayManager)
        {
            _displayManager = displayManager;
            var backgroundTexture = textureProvider.GetTextureByFile("UI/MenuBackground", "MenuBackground");
            _background = new Sprite(backgroundTexture);
            _background.Position = new Vector2(0, 0);

            _backgroundTrack = new Queue<Vector2>();
            _backgroundTrack.Enqueue(new Vector2(750, 1300));
            _backgroundTrack.Enqueue(new Vector2(950, 1500));
            _backgroundTrack.Enqueue(new Vector2(1150, 1300));
            _backgroundTrack.Enqueue(new Vector2(950, 1100));
        }

        public void Init()
        {
            _currentBackgroundTarget = _backgroundTrack.Dequeue();
            _backgroundTrack.Enqueue(_currentBackgroundTarget);
        }
        
        public void Render(RenderTarget target)
        {
            target.Draw(_background);
        }
        
        public void Tick()
        {
            var perspective = _displayManager.CurrentPerspective;
            if (perspective.TargetPosition == null)
            {
                perspective.WorldView.Center = _currentBackgroundTarget;
                SetNextTarget(perspective);
            }

            if (perspective.WorldView.Center.NearEquals(_currentBackgroundTarget, 20f))
                SetNextTarget(perspective);
        }

        private void SetNextTarget(Perspective perspective)
        {
            _currentBackgroundTarget = _backgroundTrack.Dequeue();
            _backgroundTrack.Enqueue(_currentBackgroundTarget);
            perspective.Track(_currentBackgroundTarget, 3.2f);
        }
    }
}