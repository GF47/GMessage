using System;
using System.Collections.Generic;

namespace GMessage
{
    public abstract partial class Dispatcher : IDispatcher, IDisposable
    {
        private List<IListener> _expectedListeners;

        protected Dictionary<IListener, List<int>> listeners;

        public event Action<IMessage> Receiving;

        public int ID { get; protected set; }

        public abstract IDispatcher GetDispatcher();

        public virtual void Init(int id)
        {
            ID = id;

            _expectedListeners = new List<IListener>();
            listeners = new Dictionary<IListener, List<int>>();
        }

        public virtual void Receive(IMessage message)
        {
            Receiving?.Invoke(message);

            foreach (var pair in listeners)
            {
                if (pair.Value.Contains(message.ID))
                {
                    _expectedListeners.Add(pair.Key);
                }
            }

            for (int i = 0; i < _expectedListeners.Count; i++)
            {
                _expectedListeners[i].Receive(message);
            }

            _expectedListeners.Clear();
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

        public void Dispose()
        {
            Receiving = null;

            listeners = null;
            _expectedListeners = null;

            Remove(ID);
        }

        #endregion dispose
    }
}