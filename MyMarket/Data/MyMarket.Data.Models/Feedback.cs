namespace MyMarket.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using MyMarket.Common;

    public class Feedback : BaseModel<int>
    {
        [Required]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [EmailAddress]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Email { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public bool IsFixed { get; set; }
    }
}
