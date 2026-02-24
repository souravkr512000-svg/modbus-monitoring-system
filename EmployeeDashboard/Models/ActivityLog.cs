using System.ComponentModel.DataAnnotations;

namespace EmployeeDashboard.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

        [Required]
        [StringLength(100)]
        public string Action { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? EntityType { get; set; }

        public int? EntityId { get; set; }

        [StringLength(50)]
        public string? IpAddress { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
