using FreeScape.Engine.Core.Managers;
using FreeScape.Engine.Physics.Collisions;
using FreeScape.Engine.Render.Scenes;

namespace FreeScape.Engine
{
    public class Game
    {
        private readonly SceneManager _sceneManager;
        private readonly GameManager _gameManager;
        private readonly CollisionEngine _collisionEngine;

        public Game(SceneManager sceneManager, GameManager controller, CollisionEngine collisionEngine)
        {
            _sceneManager = sceneManager;
            _gameManager = controller;
            _collisionEngine = collisionEngine;
        }
        public void Start<T>() where T : IScene
        {
            _sceneManager.SetScene<T>();
            _gameManager.Start(_sceneManager.Tick, _sceneManager.Render, _collisionEngine.CheckCollisions);
        }
    }
}