using System.Collections.Generic;
using UnityEngine;

namespace GMessage
{
    public abstract class MonoListener<T> : MonoBehaviour, IListener where T : IDispatcher
    {
        public abstract T GetDispatcher();

        IDispatcher IListener.GetDispatcher() => GetDispatcher();

        public abstract void Receive(IMessage message);

        public void Register(IList<int> messageID)
        {
            if (messageID == null || messageID.Count < 1) { return; }
            GetDispatcher().Register(this, messageID);
        }

        public void Register(params int[] messageID)
        {
            if (messageID == null || messageID.Length < 1) { return; }
            GetDispatcher().Register(this, messageID);
        }

        public void UnRegister(IList<int> messageID)
        {
            if (messageID == null || messageID.Count < 1) { return; }
            GetDispatcher().UnRegister(this, messageID);
        }

        public void UnRegister(params int[] messageID)
        {
            if (messageID == null || messageID.Length < 1) { return; }
            GetDispatcher().UnRegister(this, messageID);
        }
    }
}