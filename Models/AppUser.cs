using System.ComponentModel.DataAnnotations;

namespace PromptProfiller.Models
{
    public class AppUser : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [Required] 
        public string Username { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;

        public bool IsActive { get; set; }

    }
}
