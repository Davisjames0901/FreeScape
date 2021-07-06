using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Utilities;
using SFML.Graphics;

namespace FreeScape.Engine.Render
{
    public class Perspective : ITickable
    {
        private IGameObject _target;
        public Perspective(string name, View view)
        {
            Name = name;
            View = view;
        }
        
        public string Name { get; }
        public View View { get; }

        public void Track(IGameObject go)
        {
            _target = go;
        }
        public void Tick()
        {
            if (_target != null)
            {
                View.Center = Maths.Lerp(View.Center, _target.Position, 0.01f);
            }
        }
    }
}