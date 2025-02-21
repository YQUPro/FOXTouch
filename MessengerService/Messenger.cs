using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerService
{
    public class Messenger
    {
        private static Messenger _instance;
        public static Messenger Instance => _instance ?? (_instance = new Messenger());

        private readonly Dictionary<Type, List<Action<object>>> _subscribers = new Dictionary<Type, List<Action<object>>>();

        public void Subscribe<TMessage>(Action<TMessage> action)
        {
            if (!_subscribers.ContainsKey(typeof(TMessage)))
            {
                _subscribers[typeof(TMessage)] = new List<Action<object>>();
            }
            _subscribers[typeof(TMessage)].Add(o => action((TMessage)o));
        }

        public void Send<TMessage>(TMessage message)
        {
            if (_subscribers.ContainsKey(typeof(TMessage)))
            {
                foreach (var action in _subscribers[typeof(TMessage)])
                {
                    action(message);
                }
            }
        }
    }
}
