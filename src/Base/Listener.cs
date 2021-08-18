using System.Collections.Generic;

namespace GMessage
{
    public abstract class Listener<T> : IListener where T : IDispatcher
    {
        public abstract T GetDispatcher();
        IDispatcher IListener.GetDispatcher() => GetDispatcher();

        public abstract void Receive(IMessage message);

        public void Register(IList<int> messageID)
        {
            GetDispatcher().Register(this, messageID);
        }

        public void Register(params int[] messageID)
        {
            GetDispatcher().Register(this, messageID);
        }

        public void UnRegister(IList<int> messageID)
        {
            GetDispatcher().UnRegister(this, messageID);
        }

        public void UnRegister(params int[] messageID)
        {
            GetDispatcher().UnRegister(this, messageID);
        }
    }
}