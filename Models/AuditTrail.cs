﻿namespace PromptProfiller.Models
{
    public class AuditTrail
    {
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string? ModifiedBy { get; set; } 
        public DateTime DateModified { get; set;}
    }
}
