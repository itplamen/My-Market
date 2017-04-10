namespace MyMarket.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using MyMarket.Common;
    
    public class Country : BaseModel<int>
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ValidationConstants.NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
