using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FreeScape.Engine.Config.Action
{
    public class ActionMaps
    {
        private readonly Dictionary<string, List<MappedAction>> _actionMaps;
        private Dictionary<string, MappedAction> _currentActionMap;
        private Dictionary<string, MappedAction> _currentButtonPressedMap;
        private Dictionary<string, MappedAction> _currentButtonReleasedMap;
        private readonly string _mapDir;

        public ActionMaps(string mapDir)
        {
            _mapDir = mapDir;
            var actionMaps = new Dictionary<string, List<MappedAction>>();
            foreach (var file in Directory.EnumerateFiles(mapDir).Where(x => x.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)))
            {
                var name = file.Split(Path.DirectorySeparatorChar).Last().Split('.').First();
                actionMaps.Add(name, GetActionMap(file));
            }
            _actionMaps = actionMaps;
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
                throw new Exception($"Could not find {actionMap}.json in {_mapDir}");
            
            _currentActionMap = _actionMaps[actionMap].ToDictionary(x => x.Action, x => x);
            _currentButtonPressedMap = _actionMaps[actionMap].Where(x=>x.OnPressed).ToDictionary(x => x.Button, x => x);
            _currentButtonReleasedMap = _actionMaps[actionMap].Where(x=>x.OnReleased).ToDictionary(x => x.Button, x => x);
        }

        public MappedAction GetMappedPressedAction(string action)
        {
            return GetMappedAction(_currentButtonPressedMap, action);
        }
        public MappedAction GetMappedReleasedAction(string action)
        {
            return GetMappedAction(_currentButtonReleasedMap, action);
        }
        public MappedAction GetMappedAction(string action)
        {
            return GetMappedAction(_currentActionMap, action);
        }
        private MappedAction GetMappedAction(Dictionary<string, MappedAction> dict, string action)
        {
            if (dict == null)
                return null;
            return !dict.ContainsKey(action) ? null : dict[action];
        }
    }
}