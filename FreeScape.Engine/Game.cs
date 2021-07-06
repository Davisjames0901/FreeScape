using FreeScape.Engine.Managers;
using FreeScape.Engine.Render.Scenes;

namespace FreeScape.Engine
{
    public class Game
    {
        private readonly SceneManager _sceneManager;
        private readonly GameManager _gameManager;

        public Game(SceneManager sceneManager, GameManager controller)
        {
            _sceneManager = sceneManager;
            _gameManager = controller;
        }
        public void Start<T>() where T : IScene
        {
            _sceneManager.SetScene<T>();
            _gameManager.Start(_sceneManager.Tick, _sceneManager.Render);
        }
    }
}