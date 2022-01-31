using Model;

namespace Main.DTO
{
    public static class DtoExtensions
    {
        public static Resolution ToModel(this ResolutionDto dto) =>
            new Resolution
            {                
                Id = dto.Id,
                UserId = dto.UserId,
                Target  = dto.Target,
                TargetDate = dto.TargetDate,
                Achieved = dto.Achieved
            };

        public static ResolutionDto ToDto(this Resolution dto) =>
            new ResolutionDto
            {
                Id = dto.Id,
                UserId = dto.UserId,
                Target = dto.Target,
                TargetDate = dto.TargetDate,
                Achieved = dto.Achieved
            };
    }
}
