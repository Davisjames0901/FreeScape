using SFML.Graphics;

namespace FreeScape.Engine.Render
{
    public class Perspective
    {
        public Perspective(string name, View view)
        {
            Name = name;
            View = view;
        }
        
        public string Name { get; }
        public View View { get; }
    }
}