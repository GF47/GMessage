using System;
using System.Collections.Generic;

namespace GMessage
{
    public interface IDispatcher : IListener
    {
        event Action<IMessage> Receiving;

        int ID { get; }

        void Init(int id);

        void Register(IListener listener, IList<int> messageID);
        void UnRegister(IListener listener, IList<int> messageID);
    }
}