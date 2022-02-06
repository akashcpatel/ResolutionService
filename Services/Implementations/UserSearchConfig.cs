using System.ComponentModel.DataAnnotations;

namespace Services.Implementations
{
    internal class UserSearchConfig
    {
        public const string PositionInConfig = "UserSearch";

        [Required]
        public string BaseUrl { get; set; }

        [Required]
        public string FindUri { get; set; }
    }
}
