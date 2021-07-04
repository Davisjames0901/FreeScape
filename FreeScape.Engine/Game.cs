using System;
using System.Threading;
using FreeScape.Engine.Render;

namespace FreeScape.Engine
{
    public class Game
    {
        private readonly SceneManager _sceneManager;
        private readonly GameInfo _gameInfo;

        private bool _isRunning;

        public Game(SceneManager sceneManager, GameInfo gameInfo)
        {
            _sceneManager = sceneManager;
            _gameInfo = gameInfo;
        }
        public void Start<T>() where T : IScene
        {
            if (_isRunning)
                throw new Exception("Cant start ticker because its already running");
            _isRunning = true;
            _sceneManager.SetScene<T>();
            while (_isRunning)
            {
                Thread.Sleep(100);
                _sceneManager.Tick();
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}