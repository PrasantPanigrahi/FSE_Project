using System;
using System.Collections.Generic;

namespace PM.Models
{
    public class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsSuspended { get; set; }

        public int Priority { get; set; }

        //Foreign key for user
        public int ManagerId { get; set; }

        public virtual User Manager { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}