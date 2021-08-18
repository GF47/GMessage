using System.Collections.Generic;

namespace GMessage
{
    public interface IListener
    {
        void Receive(IMessage message);

        IDispatcher GetDispatcher();

        void Register(IList<int> messageID);

        void UnRegister(IList<int> messageID);
    }
}