using Model;

namespace Main.DTO.Converters
{
    public static class ModelToDtoConverter
    {
        public static ResolutionDto To(Resolution u)
        {
            return new ResolutionDto
            {
                //UserName = u.UserName,
                //Id = u.Id,
                //FirstName = u.FirstName,
                //LastName = u.LastName
            };
        }
    }
}