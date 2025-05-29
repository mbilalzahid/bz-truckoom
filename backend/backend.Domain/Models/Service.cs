using System.ComponentModel.DataAnnotations;


namespace backend.Domain.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }

        [Required]
        public DateTime ServiceDate { get; set; }

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
