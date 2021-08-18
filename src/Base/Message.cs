namespace GMessage
{
    public class Message : IMessage
    {
        public virtual int ID { get; protected set; }

        public virtual object Sender { get; protected set; }

        public virtual object Content { get; set; }

        public Message(int id, object sender = null, object content = null)
        {
            ID = id;
            Sender = sender;
            Content = content;
        }

        public override string ToString()
        {
            return Content.ToString();
        }
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