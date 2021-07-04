using SFML.Window;

namespace FreeScape.Engine.Event
{
    public interface IKeyListener
    {
        void KeyPressed(object sender, KeyEventArgs args);
        void KeyReleased(object sender, KeyEventArgs args);
    }
}