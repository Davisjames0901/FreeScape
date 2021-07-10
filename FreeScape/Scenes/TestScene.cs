using System;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Scenes;
using FreeScape.Layers;
using System.Collections.Generic;

namespace FreeScape.Scenes
{
    public class TestScene : LayeredScene
    {
        private readonly LayerProvider _layerProvider;
        private readonly SoundProvider _sounds;

        public TestScene(ActionProvider actionProvider, LayerProvider layerProvider, SoundProvider sounds, Movement movement):base(movement)
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
            
            Layers.Add(map);
            Layers.Add(entityLayer);

            _sounds.PlaySound("desert");
        }

        public override void Dispose()
        {
        }
    }
}