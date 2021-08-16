using System.Collections.Generic;
using System;

namespace GMessage
{
    public abstract partial class Dispatcher
    {
        public const int GLOBAL = 0;

        private static readonly Dictionary<int, IDispatcher> _dispatchers = new Dictionary<int, IDispatcher>();

        public static T Instance<T>(int id) where T : IDispatcher
        {
            return (T)_dispatchers[id];
        }

        public static T Add<T>(int id) where T : IDispatcher, new()
        {
            var dispatcher = new T();

            dispatcher.Init(id);

            _dispatchers.Add(dispatcher.ID, dispatcher);

            return dispatcher;
        }

        public static void Remove(int id)
        {
            _dispatchers.Remove(id);
        }
    }
}
