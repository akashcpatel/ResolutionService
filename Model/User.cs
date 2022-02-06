using System;

namespace Model
{
    [Serializable]
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User Clone()
        {
            return new User
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName
            };
        }

        public override bool Equals(object obj)
        {
            if (!(obj is User))
                return false;

            if ((obj == null && this != null) || (obj != null && this == null))
                return false;

            var rhs = (User)obj;

            return Id == rhs.Id && FirstName == rhs.FirstName && LastName == rhs.LastName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
