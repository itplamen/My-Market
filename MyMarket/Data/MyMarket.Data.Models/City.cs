namespace MyMarket.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;
    using MyMarket.Common;
    
    public class City : BaseModel<int>
    {
        [Required]
        [Index(IsUnique = true)]
        [MinLength(ValidationConstants.NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}