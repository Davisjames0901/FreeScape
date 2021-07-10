using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Scenes;
using FreeScape.Layers;
using SFML.Graphics;

namespace FreeScape.Scenes
{
    public class MainMenuScene : LayeredScene
    {

        private readonly LayerProvider _layerProvider;
        private readonly SoundProvider _sounds;
        public MainMenuScene(ActionProvider actionProvider, LayerProvider layerProvider,  SoundProvider sounds)
        {

            actionProvider.SwitchActionMap("MainMenu");
            _layerProvider = layerProvider;
            _sounds = sounds;
        }
        public override void Init()
        {
            Layers.Add(_layerProvider.Provide<MainMenuHome>());
            Layers.Add(_layerProvider.Provide<MainMenuOptions>());
            _sounds.PlaySound("desert");
        }



        public override void Dispose()
        {

        }

    }
}