using System.ComponentModel.DataAnnotations;

namespace Publisher
{
    internal class PublisherConfig
    {
        public const string PositionInConfig = "Publisher";

        [Required]
        public string UserChangedQueue { get; set; }

        [Required]
        public string ResolutionChangedQueue { get; set; }
    }
}
