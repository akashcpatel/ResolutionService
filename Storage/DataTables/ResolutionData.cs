using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.DataTables
{
    public class ResolutionData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
