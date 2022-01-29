using Model;

namespace Main.DTO
{
    public static class DtoExtensions
    {
        public static Resolution To(this ResolutionDto dto) =>
            new Resolution
            {                
                Id = dto.Id,
                UserId = dto.UserId,
                Target  = dto.Target,
                TargetDate = dto.TargetDate,
                Achieved = dto.Achieved
            };
    }
}
