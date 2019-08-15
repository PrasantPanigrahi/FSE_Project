using System.ComponentModel.DataAnnotations;

namespace PM.Extensions.DTO
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        public string DisplayName { get { return string.Join(" ", FirstName, LastName); } }
    }
}