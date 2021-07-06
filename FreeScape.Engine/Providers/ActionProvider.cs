using System;
using System.Collections.Generic;
using FreeScape.Engine.Config.Action;
using FreeScape.Engine.Managers;
using SFML.Window;

namespace FreeScape.Engine.Providers
{
    public class ActionProvider
    {
        private readonly ActionMaps _actionMap;
        private readonly SfmlActionResolver _actionResolver;
        private readonly List<Action<string>> _actionPressedSubscribers;
        private readonly List<Action<string>> _actionReleasedSubscribers;
        public ActionProvider(GameInfo info, SfmlActionResolver actionResolver, DisplayManager displayManager)
        {
            _actionResolver = actionResolver;
            _actionPressedSubscribers = new List<Action<string>>();
            _actionReleasedSubscribers = new List<Action<string>>();
            _actionMap = new ActionMaps(info.ActionMapDirectory);
            displayManager.RegisterOnPressed(OnKeyPressed);
            displayManager.RegisterOnReleased(OnKeyReleased);
        }

        public void SwitchActionMap(string map)
        {
            _actionMap.SwitchActionMap(map);
        }
        
        public bool IsActionActivated(string action)
        {
            var mappedAction = _actionMap.GetMappedAction(action);
            return ConvertActionAndCheckIfActioned(mappedAction);
        }

        public void SubscribeOnPressed(Action<string> callback)
        {
            _actionPressedSubscribers.Add(callback);
        }

        public void SubscribeOnReleased(Action<string> callback)
        {
            _actionReleasedSubscribers.Add(callback);
        }

        private bool ConvertActionAndCheckIfActioned(MappedAction action)
        {
            switch (action?.Device)
            {
                case "mouse":
                    return CheckMouseAction(action);
                case "keyboard":
                    return CheckKeyboardAction(action);
                case null:
                    return false;
                default:
                    throw new Exception(
                        $"Unknown device type in Action: {action.Action}, Device: {action.Device}");
            }
        }

        private bool CheckMouseAction(MappedAction action)
        {
            var button = _actionResolver.GetMouseButton(action.Button);
            return Mouse.IsButtonPressed(button);
        }

        private bool CheckKeyboardAction(MappedAction action)
        {
            var key = _actionResolver.GetKeyboardKey(action.Button);
            return Keyboard.IsKeyPressed(key);
        }

        private void Notify(List<Action<string>> subscribers, MappedAction action)
        {
            foreach (var subscriber in subscribers)
                subscriber(action.Action);
        }

        private void OnKeyPressed(object sender, KeyEventArgs args)
        {
            var actionName = _actionResolver.GetAction(args.Code);
            var action = _actionMap.GetMappedPressedAction(actionName);
            if(action == null)
                return;
            Notify(_actionPressedSubscribers, action);
        }

        private void OnKeyReleased(object sender, KeyEventArgs args)
        {
            var actionName = _actionResolver.GetAction(args.Code);
            var action = _actionMap.GetMappedPressedAction(actionName);
            if(action == null)
                return;
            Notify(_actionReleasedSubscribers, action);
        }
    }
}