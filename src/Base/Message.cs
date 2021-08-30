namespace GMessage
{
    public struct Message : IMessage
    {
        public int ID { get; private set; }

        public object Sender { get; private set; }

        public object Content { get; set; }

        public Message(int id, object sender = null, object content = null)
        {
            ID = id;
            Sender = sender;
            Content = content;
        }

        public override string ToString() => Content?.ToString();
    }

    public static class MessageUtil
    {
        public static void DispatchTo(this IMessage message, IListener listener)
        {
            listener.Receive(message);
        }

        public static void DispatchTo(this IMessage message, int dispatcherID)
        {
            var dispatcher = Dispatcher.Instance<Dispatcher>(dispatcherID);
            dispatcher.Receive(message);
        }

        public static void Dispatch(this IMessage message)
        {
            GlobalDispatcher.Instance.Receive(message);
        }
    }
}