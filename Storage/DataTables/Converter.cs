using Model;

namespace Storage.DataTables
{
    internal static class ModelExtensions
    {
        public static ResolutionData ResolutionToResolutionData(this Resolution u) =>
            new ResolutionData
            {
                Id = u.Id,
                UserId = u.UserId,
                Target = u.Target,
                TargetDate = u.TargetDate,
                Achieved = u.Achieved
            };

        public static Resolution ResolutionDataToResolution(this ResolutionData u) =>
            new Resolution
            {
                Id = u.Id,
                UserId = u.UserId,
                Target = u.Target,
                TargetDate = u.TargetDate,
                Achieved = u.Achieved
            };
    }
}