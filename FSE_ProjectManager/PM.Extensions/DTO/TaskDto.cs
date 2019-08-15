using System.ComponentModel.DataAnnotations;

namespace PM.Extensions.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string EndDate { get; set; }

        [Required]
        public int Priority { get; set; }

        public int? ParentTaskId { get; set; }

        public string ParentTaskName { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int StatusId { get; set; }
    }
}