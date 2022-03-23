using System;

namespace GMessage
{
    public class MiniListener : Listener<IDispatcher>
    {
        public event Action<IMessage> Receiving;

        public int DispatcherID { get; private set; }

        public MiniListener(int dispatcherID) => DispatcherID = dispatcherID;

        public override IDispatcher GetDispatcher() => Dispatcher.Instance<IDispatcher>(DispatcherID);

        public override void Receive(IMessage message) => Receiving?.Invoke(message);
    }

    public class MiniDispatcher : Dispatcher
    {
        public override IDispatcher GetDispatcher() => GlobalDispatcher.Instance;
    }
}