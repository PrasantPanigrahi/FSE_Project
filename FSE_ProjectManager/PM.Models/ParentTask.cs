using System.Collections.Generic;

namespace PM.Models
{
    public class ParentTask
    {
        public ParentTask()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}