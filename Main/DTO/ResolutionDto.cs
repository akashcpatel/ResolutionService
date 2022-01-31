using Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace Main.DTO
{
    public class ResolutionDto : IValidate
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Target { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }

        [Required]
        public bool Achieved { get; set; } = false;

        public void Validate()
        {
            Guard.Id(Id);
            Guard.UserId(UserId);
            Guard.Target(Target);
            Guard.TargetDate(TargetDate);
        }

        public override string ToString()
        {
            return $"ResolutionDto[{nameof(Id)} = {Id}, {nameof(UserId)} = {UserId}, {nameof(Target)} = {Target}, {nameof(TargetDate)} = {TargetDate}, {nameof(Achieved)} = {Achieved}]";
        }
    }
}
