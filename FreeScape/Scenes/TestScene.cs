using System;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Scenes;
using FreeScape.Layers;

namespace FreeScape.Scenes
{
    public class TestScene : LayeredScene
    {
        private readonly DisplayManager _displayManager;
        private readonly LayerProvider _layerProvider;
        private readonly SoundProvider _sounds;

        public TestScene(DisplayManager displayManager, ActionProvider actionProvider, LayerProvider layerProvider, SoundProvider sounds)
        {
            _displayManager = displayManager;
            _layerProvider = layerProvider;
            _sounds = sounds;
            actionProvider.SwitchActionMap("Player");
            actionProvider.SubscribeOnPressed(x =>
            {
                if (x == "LeftClick")
                {
                    Console.WriteLine($"Fuck Yeah {actionProvider.GetMouseWorldCoods()}");
                }
            });
        }

        public override void Init()
        {
            var player = _layerProvider.Provide<Player>();
            Layers.Add(player);
            _displayManager.Track(x=> x.Name == "main", player);

            var map = _layerProvider.Provide<TestTileMap>();
            Layers.Add(map);
            
            _sounds.PlaySound("desert");
        }

        public override void Dispose()
        {
        }
    }
}