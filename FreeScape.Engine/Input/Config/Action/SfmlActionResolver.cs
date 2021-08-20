using System;
using SFML.Window;

namespace FreeScape.Engine.Input.Config.Action
{
    public class SfmlActionResolver
    {
        public Keyboard.Key GetKeyboardKey(string action)
        {
            return Enum.Parse<Keyboard.Key>(action);
        }

        public Mouse.Button GetMouseButton(string action)
        {
            return Enum.Parse<Mouse.Button>(action);
        }

        public Mouse.Wheel GetMouseWheel(string action)
        {
            return Enum.Parse<Mouse.Wheel>(action);
        }

        public string GetAction(Keyboard.Key key)
        {
            return key.ToString();
        }
        
        public string GetAction(Mouse.Button button)
        {
            return button.ToString();
        }
        
        public string GetAction(Mouse.Wheel wheel)
        {
            return wheel.ToString();
        }
    }
}