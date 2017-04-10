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
        [MaxLength(ValidationConstants.CONTENT_MAX_LENGHT)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public int AdId { get; set; }

        [ForeignKey("AdId")]
        public virtual Ad Ad { get; set; }
    }
}
