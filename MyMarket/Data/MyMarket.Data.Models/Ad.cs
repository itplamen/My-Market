namespace MyMarket.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using MyMarket.Common;

    public class Ad : BaseModel<int>
    {
        public Ad()
        {
            this.Visits = new HashSet<Visit>();
            this.Likes = new HashSet<Like>();
            this.Comments = new HashSet<Comment>();
            this.Flags = new HashSet<Flag>();
        }

        [Required]
        [MinLength(ValidationConstants.NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.CONTENT_MIN_LENGTH)]
        public string Description { get; set; }

        public byte[] Picture { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Flag> Flags { get; set; }
    }
}
