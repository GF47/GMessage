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
}