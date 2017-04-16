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
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MinLength(ValidationConstants.NameMinLength)]
        [MaxLength(ValidationConstants.NameMaxLenght)]
        public string Title { get; set; }

        [Required]
        [MinLength(ValidationConstants.ContentMinLength)]
        [MaxLength(ValidationConstants.ContentMaxLength)]
        public string Description { get; set; }

        public int? MainImageId { get; set; }

        public virtual Image MainImage { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        public string UserId { get; set; }
 
        public virtual User User { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Flag> Flags { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
