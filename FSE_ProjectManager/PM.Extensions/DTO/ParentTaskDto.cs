using System.ComponentModel.DataAnnotations;

namespace PM.Extensions.DTO
{
    public class ParentTaskDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
