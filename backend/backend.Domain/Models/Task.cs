using System.ComponentModel.DataAnnotations;


namespace backend.Domain.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(100)]
        public string TaskName { get; set; }

        public string? Description { get; set; }

        public string? Remarks { get; set; }
    }
}
