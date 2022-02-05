using Model;

namespace Storage.DataTables
{
    internal static class ModelExtensions
    {
        public static ResolutionData ToData(this Resolution r) =>
            new ResolutionData
            {
                Id = r.Id,
                UserId = r.UserId,
                Target = r.Target,
                TargetDate = r.TargetDate,
                Achieved = r.Achieved
            };

        public static Resolution ToModel(this ResolutionData r) =>
            new Resolution
            {
                Id = r.Id,
                UserId = r.UserId,
                Target = r.Target,
                TargetDate = r.TargetDate,
                Achieved = r.Achieved
            };

        public static UserData ToData(this User u) =>
            new UserData
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            };

        public static User ToModel(this UserData u) =>
            new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName
            };
    }
}