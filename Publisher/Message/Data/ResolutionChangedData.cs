using Model;

namespace Publisher.Message.Data
{
    public class ResolutionChangedData
    {
        public Header Header;
        public Resolution Payload;

        public static ResolutionChangedData Create(Header header, Resolution payload)
        {
            return new ResolutionChangedData { Header = header, Payload = payload };
        }
    }
}
