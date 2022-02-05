namespace Publisher
{
    public interface IPublisherFactory
    {
        IUserChangedReceiver GetUserChangedReceiver();
    }
}
