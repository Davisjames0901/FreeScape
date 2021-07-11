using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Scenes;
using FreeScape.Layers;

namespace FreeScape.Scenes
{
    public class TestScene : LayeredScene
    {
        private readonly LayerProvider _layerProvider;
        private readonly SoundProvider _sounds;

        public TestScene(ActionProvider actionProvider, LayerProvider layerProvider, SoundProvider sounds)
        {
            _layerProvider = layerProvider;
            _sounds = sounds;
            actionProvider.SwitchActionMap("Player");
            actionProvider.SubscribeOnPressed(x =>
            {
                if (x == "LeftClick")
                {
                    actionProvider.GetMouseWorldCoods();
                }
            });
        }


        public override void Init()
        {

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