using System;
using System.Collections.Generic;

namespace GMessage
{
    public abstract partial class Dispatcher : IDispatcher, IDisposable
    {
        // private List<IListener> _expectedListeners; // TODO BUG 当消息内嵌套消息时，会重复发送或者向无关的监听者发送

        private const int MAX_LAYER = 4;
        private int _currentLayer = -1;
        private List<IListener>[] _expectedListeners;

        protected Dictionary<IListener, List<int>> listeners;

        public Action<IMessage> Receiving;

        public int ID { get; protected set; }

        public abstract IDispatcher GetDispatcher();

        public virtual void Init(int id)
        {
            ID = id;

            _expectedListeners = new List<IListener>[MAX_LAYER]; // 嵌套多层就多少有点大病了……
            for (int i = 0; i < MAX_LAYER; i++)
            {
                _expectedListeners[i] = new List<IListener>();
            }
            listeners = new Dictionary<IListener, List<int>>();
        }

        public virtual void Receive(IMessage message)
        {
            Receiving?.Invoke(message);
            Dispatch(message);
        }

        public void Dispatch(IMessage message)
        {
            _currentLayer++;

            if (_currentLayer >= MAX_LAYER)
            {
                throw new Exception($"Module {ID} dispatch layer > {MAX_LAYER - 1}, do not do that.");
            }

            var list = _expectedListeners[_currentLayer];

            foreach (var pair in listeners)
            {
                if (pair.Value.Contains(message.ID))
                {
                    list.Add(pair.Key);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Receive(message);
            }

            list.Clear();

            _currentLayer--;
        }

        public void Register(IListener listener, IList<int> messageID)
        {
            if (listeners.TryGetValue(listener, out List<int> list))
            {
                for (int i = 0; i < messageID.Count; i++)
                {
                    if (list.Contains(messageID[i])) { continue; }
                    else { list.Add(messageID[i]); }
                }
            }
            else
            {
                listeners.Add(listener, new List<int>(messageID));
            }
        }

        public void UnRegister(IListener listener, IList<int> messageID)
        {
            if (listeners.TryGetValue(listener, out List<int> list))
            {
                for (int i = 0; i < messageID.Count; i++)
                {
                    if (!list.Contains(messageID[i])) { continue; }
                    else { list.Remove(messageID[i]); }
                }
            }
        }

        public void Register(IList<int> messageID)
        {
            GetDispatcher()?.Register(this, messageID);
        }

        public void UnRegister(IList<int> messageID)
        {
            GetDispatcher()?.UnRegister(this, messageID);
        }

        #region dispose

        public virtual void Dispose()
        {
            listeners = null;
            _expectedListeners = null;

            Remove(ID);
        }

        #endregion dispose
    }
}