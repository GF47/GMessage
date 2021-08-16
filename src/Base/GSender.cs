using GMessage;

public static class GSender
{
    public static void Send(IDispatcher dispatcher, int messageID, object sender = null, object content = null)
    {
        dispatcher.Receive(new Message(messageID, sender, content));
    }

    public static void Send(int dispatcherID, int messageID, object sender=null, object content = null)
    {
        var dispatcher = Dispatcher.Instance<Dispatcher>(dispatcherID);

        dispatcher?.Receive(new Message(messageID, sender, content));
    }

    public static void Send(int messageID, object sender = null, object content = null)
    {
        GlobalDispatcher.Instance.Receive(new Message(messageID, sender, content));
    }
}
