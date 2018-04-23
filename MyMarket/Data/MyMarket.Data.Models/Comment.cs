namespace MyMarket.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using MyMarket.Common;

    public class Comment : BaseModel<int>
    {
        [Required]
        [MinLength(ValidationConstants.CONTENT_MIN_LENGTH)]
        [MaxLength(ValidationConstants.CONTENT_MAX_LENGTH)]
        public string Content { get; set; }

        [Required]
        public int AdId { get; set; }

        [ForeignKey("AdId")]
        public virtual Ad Ad { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}