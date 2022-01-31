using System;

namespace Publisher.Message
{
    public class Header
    {
        public Guid Key;
        public ChangeType ChangeType;

        public static Header Create(Guid key, ChangeType changeType)
        {
            return new Header
            {
                Key = key,
                ChangeType = changeType
            };
        }
    }
}