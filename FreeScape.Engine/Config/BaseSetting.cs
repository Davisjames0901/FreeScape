using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FreeScape.Engine.Config
{
    public abstract class BaseSetting: IDisposable
    {
        [JsonIgnore]
        private List<System.Action> _onChangedSubscribers;
        
        [JsonIgnore] 
        internal string FilePath { get; set; }

        protected BaseSetting()
        {
            _onChangedSubscribers = new List<System.Action>();
        }

        public void Subscribe(System.Action action)
        {
            _onChangedSubscribers.Add(action);
        }

        protected void Changed()
        {
            foreach (var a in _onChangedSubscribers)
                a();
        }

        public abstract void Dispose();
    }
}