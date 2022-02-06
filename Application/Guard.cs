using System;

namespace Application
{
    public static class Guard
    {
        public static void Id(Guid id)
        {
            if (id == null || id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} is Null or Empty");
        }

        public static void UserId(Guid userId)
        {
            if (userId == null || userId == Guid.Empty)
                throw new ArgumentException($"{nameof(userId)} is Null or Empty");
        }

        public static void Target(string target)
        {
            if (string.IsNullOrWhiteSpace(target))
                throw new ArgumentException($"{nameof(target)} is Null or Empty");
        }

        public static void TargetDate(DateTime targetDate)
        {
            if (targetDate == null || targetDate == DateTime.MinValue)
                throw new ArgumentException($"{nameof(targetDate)} is Null or Empty");
        }
    }
}
