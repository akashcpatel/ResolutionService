using Model;

namespace Publisher.Message.Data
{
    public class UserChangedData
    {
        public Header Header;
        public User Payload;

        public static UserChangedData Create(Header header, User payload)
        {
            return new UserChangedData { Header = header, Payload = payload };
        }
    }
}
