using System;
using System.Threading;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Render;
using FreeScape.Engine.Render.Scenes;

namespace FreeScape.Engine
{
    public class Game
    {
        private readonly SceneManager _sceneManager;
        private readonly GameInfo _gameInfo;
        private readonly Controller _controller;

        private bool _isRunning;

        public Game(SceneManager sceneManager, GameInfo gameInfo, Controller controller)
        {
            _sceneManager = sceneManager;
            _gameInfo = gameInfo;
            _controller = controller;
        }
        public void Start<T>() where T : IScene
        {
            if (_isRunning)
                throw new Exception("Cant start ticker because its already running");
            _isRunning = true;
            _sceneManager.SetScene<T>();


            _controller.Start(_sceneManager.Tick, _sceneManager.Render);

        }

        

        public void Stop()
        {
            _isRunning = false;
        }
    }
}