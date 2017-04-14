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
        [MinLength(ValidationConstants.NameMinLength)]
        [MaxLength(ValidationConstants.NameMaxLenght)]
        public string Name { get; set; }

        public virtual ICollection<Ad> Ads { get; set; }
    }
}
