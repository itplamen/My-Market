namespace MyMarket.Web.Models.Ad
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using Category;
    using Common;
    using Common.Validators;
    using Data.Models;
    using Infrastructure.Mapping;

    public class AdCreateViewModel : IMapFrom<Ad>
    {
        [Required]
        [MinLength(ValidationConstants.NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.NAME_MAX_LENGTH)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [MinLength(ValidationConstants.CONTENT_MIN_LENGTH)]
        [MaxLength(ValidationConstants.CONTENT_MAX_LENGTH)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        [Required]
        [Display(Name = "Main Picture")]
        [CustomValidation(typeof(AdImageValidator), "ValidateImage")]
        public HttpPostedFileBase MainPicture { get; set; }

        [Display(Name = "Other Pictures")]
        [CustomValidation(typeof(AdImageValidator), "ValidateImages")]
        public IEnumerable<HttpPostedFileBase> OtherPictures { get; set; }
    }
}