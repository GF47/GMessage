using System.Collections.Generic;

namespace GMessage
{
    public interface IDispatcher : IListener
    {
        int ID { get; }

        void Init(int id);

        void Dispatch(IMessage message);

        void Register(IListener listener, IList<int> messageID);

        void UnRegister(IListener listener, IList<int> messageID);
    }
}