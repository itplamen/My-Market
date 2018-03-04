namespace MyMarket.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using MyMarket.Common;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Ads = new HashSet<Ad>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ValidationConstants.NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        public virtual ICollection<Ad> Ads { get; set; }
    }
}
