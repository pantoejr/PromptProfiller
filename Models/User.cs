using System.ComponentModel.DataAnnotations;

namespace PromptProfiller.Models
{
    public class User : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required] 
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string IPAddress { get; set; } = string.Empty;
        [Required]
        public string Branch { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
