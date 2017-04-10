namespace MyMarket.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class CommentFlag : IAuditInfo
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public int CommentId { get; set; }

        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
