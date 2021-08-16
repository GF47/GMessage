namespace GMessage
{
    public interface IMessage
    {
        int ID { get; }

        object Sender { get; }

        object Content { get; set; }
    }
}