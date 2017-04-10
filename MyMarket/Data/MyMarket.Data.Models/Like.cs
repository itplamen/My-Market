namespace MyMarket.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Like : IAuditInfo
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public int AdId { get; set; }

        [ForeignKey("AdId")]
        public virtual Ad Ad { get; set; }

        /// <summary>
        /// If UserId is null, therefore an anonymous user has made that like.
        /// </summary>
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
