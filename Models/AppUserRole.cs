using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromptProfiller.Models
{
    public class AppUserRole : AuditTrail
    {
        [Key]
        public int Id { get; set; }
        public int AppUserID { get; set; }
        [ForeignKey(nameof(AppUserID))]
        public virtual AppUser? AppUser { get; set; }

        public int RoleID { get; set; }
        [ForeignKey(nameof(RoleID))]
        public virtual Role? Role { get; set; }
    }
}
