using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Scenes;
using FreeScape.Layers;

namespace FreeScape.Scenes
{
    public class MainMenuScene : LayeredScene
    {

        private readonly LayerProvider _layerProvider;
        private readonly SoundProvider _sounds;
        private readonly DisplayManager _displayManager;
        public MainMenuScene(ActionProvider actionProvider, LayerProvider layerProvider,  SoundProvider sounds, DisplayManager displayManager)
        {
            actionProvider.SwitchActionMap("MainMenu");
            _displayManager = displayManager;
            _layerProvider = layerProvider;
            _sounds = sounds;
        }


        public override void Init()
        {
            Active = true;
            Layers.Add(_layerProvider.Provide<MainMenuHome>());
            Layers.Add(_layerProvider.Provide<MainMenuOptions>());
            Layers.Add(_layerProvider.Provide<MainMenuBackground>());
            _sounds.PlayMusic("intro");
        }

        public override void Dispose()
        {
        }

    }
}