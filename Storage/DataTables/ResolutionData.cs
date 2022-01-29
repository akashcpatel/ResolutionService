using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.DataTables
{
    [Table("ResolutionData")]
    public class ResolutionData
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Target { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }

        [Required]
        public bool Achieved { get; set; }
    }
}
