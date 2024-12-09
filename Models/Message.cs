using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromptProfiller.Models
{
    public class Message : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string ImagePath { get; set; } = string.Empty;

        [Required]
        public int DisplayTime { get; set; }
        public int UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public virtual User? User { get; set; }

    }
}
