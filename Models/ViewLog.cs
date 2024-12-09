using System.ComponentModel.DataAnnotations;

namespace PromptProfiller.Models
{
    public class ViewLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string IPAddress { get; set; } = string.Empty;
        [Required]
        public string ImagePath { get; set; } = string.Empty;
        public bool HasBeenView { get; set; } = false;
        public DateTime ViewDate { get; set; }
    }
}
