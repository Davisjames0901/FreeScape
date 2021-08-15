using SFML.Graphics;

namespace FreeScape.Engine.Config
{
    public class TextureInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public bool Smooth { get; set; }

        public IntRect Location => new IntRect(X, Y, Width, Height);

        public override string ToString()
        {
            return $"[Texture]: {Name}, file: {File}, location: {Location}";
        }
    }
}