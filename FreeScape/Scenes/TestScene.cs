using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Scenes;
using FreeScape.Layers;
using SFML.Graphics;

namespace FreeScape.Scenes
{
    public class TestScene : LayeredScene
    {
        private readonly LayerProvider _layerProvider;
        private readonly SoundProvider _sounds;
        private PauseMenu _pauseMenu;

        public TestScene(ActionProvider actionProvider, LayerProvider layerProvider, SoundProvider sounds)
        {
            _layerProvider = layerProvider;
            _sounds = sounds;
            actionProvider.SwitchActionMap("Player");
        }

        public override void Init()
        {
            var map = _layerProvider.Provide<TestTileMap>();
            var entityLayer = _layerProvider.Provide<EntityLayer>();
            var playerUI = _layerProvider.Provide<PlayerUI>();
            _pauseMenu = _layerProvider.Provide<PauseMenu>();
            Layers.Add(map);
            Layers.Add(entityLayer);
            Layers.Add(playerUI);
            Layers.Add(_pauseMenu);

            _sounds.PlayMusic("smooth");
        }

        public override void Tick()
        {
            if (_pauseMenu.IsPaused)
            {
                _pauseMenu.Tick();
                return;
            }
            
            foreach (var layer in Layers)
            {
                layer.Tick();
            }
        }

        public override void Dispose()
        {
        }
    }
}