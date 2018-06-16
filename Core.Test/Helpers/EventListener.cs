using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace phirSOFT.Applications.MusicStand.Core.Test.Helpers
{
    class EventListener<T> where T : EventArgs
    {
        private readonly List<T> _savedArgs = new List<T>();

        public EventListener(object raiser, string eventName)
        {
            EventInfo eventInfo = raiser.GetType().GetEvent(eventName);
            var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, "EventHandler");
            eventInfo.AddEventHandler(raiser, handler);            
        }

        private void EventHandler(object sender, T args)
        {
            _savedArgs.Add(args);
        }

        public IList<T> SavedArgs => _savedArgs;
    }
}
