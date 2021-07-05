using System.Collections.Generic;
using SFML.Window;

namespace FreeScape.Engine.Actions
{
    public class EventManager
    {
        private readonly List<IKeyListener> _keyListeners;
        public EventManager()
        {
            _keyListeners = new List<IKeyListener>();
        }

        public void RegisterEventListener(IKeyListener keyListener)
        {
            _keyListeners.Add(keyListener);
        }

        public void TriggerKeyPressed(object sender, KeyEventArgs args)
        {
            foreach (var listener in _keyListeners)
            {
                listener.KeyPressed(sender, args);
            }
        }
        
        public void TriggerKeyReleased(object sender, KeyEventArgs args)
        {
            foreach (var listener in _keyListeners)
            {
                listener.KeyReleased(sender, args);
            }
        }
    }
}