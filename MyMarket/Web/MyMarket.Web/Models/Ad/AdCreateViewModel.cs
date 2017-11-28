namespace MyMarket.Web.Models.Ad
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using Category;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Validation;

    public class AdCreateViewModel : IMapFrom<Ad>
    {
        [Required]
        [AllowHtml]
        [MinLength(ValidationConstants.NameMinLength)]
        [MaxLength(ValidationConstants.NameMaxLenght)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [MinLength(ValidationConstants.ContentMinLength)]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        [Display(Name = "Main Picture")]
        [CustomValidation(typeof(AdImageValidator), "ValidateImage")]
        public HttpPostedFileBase MainPicture { get; set; }

        [Display(Name = "Other Pictures")]
        [CustomValidation(typeof(AdImageValidator), "ValidateImages")]
        public IEnumerable<HttpPostedFileBase> OtherPictures { get; set; }
    }
}