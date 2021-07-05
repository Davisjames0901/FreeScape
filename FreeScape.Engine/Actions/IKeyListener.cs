using SFML.Window;

namespace FreeScape.Engine.Actions
{
    public interface IKeyListener
    {
        void KeyPressed(object sender, KeyEventArgs args);
        void KeyReleased(object sender, KeyEventArgs args);
    }
}