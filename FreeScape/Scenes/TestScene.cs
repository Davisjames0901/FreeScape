using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Scenes;
using FreeScape.Layers;

namespace FreeScape.Scenes
{
    public class TestScene : LayeredScene
    {
        private readonly LayerProvider _layerProvider;
        private readonly SoundProvider _sounds;
        private MainMenuHome _pauseScreen;

        public TestScene(ActionProvider actionProvider, LayerProvider layerProvider, SoundProvider sounds)
        {
            _layerProvider = layerProvider;
            _sounds = sounds;
            actionProvider.SwitchActionMap("Player");
            actionProvider.SubscribeOnPressed(x =>
            {
                if (x == "Pause")
                {
                    if (Layers.Contains(_pauseScreen))
                        Layers.Remove(_pauseScreen);
                    else
                        Layers.Add(_pauseScreen);
                }
            });
        }

        public override void Init()
        {
            _pauseScreen = _layerProvider.Provide<MainMenuHome>();
            var map = _layerProvider.Provide<TestTileMap>();
            var entityLayer = _layerProvider.Provide<EntityLayer>();
            var playerUI = _layerProvider.Provide<PlayerUI>();
            Layers.Add(map);
            Layers.Add(entityLayer);
            Layers.Add(playerUI);

            _sounds.PlayMusic("cave");
        }

        public override void Dispose()
        {
        }
    }
}