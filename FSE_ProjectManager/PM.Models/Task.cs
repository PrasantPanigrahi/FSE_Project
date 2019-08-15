using System;

namespace PM.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Priority { get; set; }

        public int StatusId { get; set; }

        public int? ParentTaskId { get; set; }

        public virtual ParentTask ParentTask { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}