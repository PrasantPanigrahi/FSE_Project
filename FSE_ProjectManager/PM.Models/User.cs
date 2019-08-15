using System.Collections.Generic;

namespace PM.Models
{
    public class User
    {
        public User()
        {
            Tasks = new HashSet<Task>();
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmployeeId { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}