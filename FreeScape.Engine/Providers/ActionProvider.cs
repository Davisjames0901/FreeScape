using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FreeScape.Engine.Actions;
using SFML.Window;

namespace FreeScape.Engine.Providers
{
    public class ActionProvider
    {
        private readonly GameInfo _info;
        private readonly SfmlActionResolver _actionResolver;
        private readonly Dictionary<string, List<MappedAction>> _actionMaps;
        private Dictionary<string, MappedAction> _currentMap;
        private string _currentMapLocation;
        public ActionProvider(GameInfo info, SfmlActionResolver actionResolver)
        {
            _info = info;
            _actionResolver = actionResolver;
            _actionMaps = new Dictionary<string, List<MappedAction>>();
            foreach (var file in Directory.EnumerateFiles(info.ActionMapDirectory).Where(x => x.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)))
            {
                var name = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
                _actionMaps.Add(name, GetActionMap(file));
            }
        }

        private List<MappedAction> GetActionMap(string path)
        {
            var text = File.ReadAllText(path);
            var descriptor = JsonSerializer.Deserialize<List<MappedAction>>(text);
            return descriptor;
        }

        public void SwitchActionMap(string actionMap)
        {
            if (!_actionMaps.ContainsKey(actionMap))
                throw new Exception($"Could not find {actionMap}.json in {_info.ActionMapDirectory}");
            
            _currentMap = _actionMaps[actionMap].ToDictionary(x => x.Action, x => x);
            _currentMapLocation = $"{_info.ActionMapDirectory}{Path.DirectorySeparatorChar}{actionMap}.json";
        }

        public bool IsActionActivated(string action)
        {
            if (_currentMap == null)
                throw new Exception("There is no action map selected");
            if(!_currentMap.ContainsKey(action))
                ConvertActionAndThrowException(action);
            var mappedAction = _currentMap[action];
            
            return ConvertActionAndCheckIfActioned(mappedAction);
        }

        private void ConvertActionAndThrowException(string action)
        {
            
        }

        private bool ConvertActionAndCheckIfActioned(MappedAction action)
        {
            switch (action.Device)
            {
                case "mouse":
                    return CheckMouseAction(action);
                case "keyboard":
                    return CheckKeyboardAction(action);
                case "wheel":
                    return CheckWheelAction(action);
                default:
                    throw new Exception(
                        $"Unknown device type in File: {_currentMapLocation}, Action: {action.Action}, Device: {action.Device}");
            }
        }

        private bool CheckMouseAction(MappedAction action)
        {
            var button = _actionResolver.GetMouseButton(action.Button);
            return Mouse.IsButtonPressed(button);
        }

        private bool CheckWheelAction(MappedAction action)
        {
            //Todo: I dont think this actually makes sense.
            return false;
        }

        private bool CheckKeyboardAction(MappedAction action)
        {
            var key = _actionResolver.GetKeyboardKey(action.Button);
            return Keyboard.IsKeyPressed(key);
        }
    }
}