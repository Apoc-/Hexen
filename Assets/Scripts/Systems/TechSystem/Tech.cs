using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Systems.TechTreeSystem
{
    public class Tech
    {
        public static Tech Define => new Tech();

        private string _name = string.Empty;
        private string _description = string.Empty;
        private int _level = 0;
        private bool _unlocked = false;
        private List<Tech> _unlocks = new List<Tech>();

        public Tech WithName(string name)
        {
            _name = name;
            return this;
        }

        public Tech WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public Tech Unlocks(Tech tech)
        {
            _unlocks.Add(tech);
            return this;
        }

        public void Unlock()
        {
            _unlocked = true;
        }

        public void Lock()
        {
            _unlocked = false;
        }
        
        
    }
}