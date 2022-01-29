using System;

namespace Model
{
    public class Resolution
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Target { get; set; }
        public DateTime TargetDate { get; set; }
        public bool Achieved { get; set; } = false;
    }
}