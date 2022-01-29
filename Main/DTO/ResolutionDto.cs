using Services;
using System;

namespace Main.DTO
{
    public class ResolutionDto : IValidate
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Target { get; set; }
        public DateTime TargetDate { get; set; }
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
