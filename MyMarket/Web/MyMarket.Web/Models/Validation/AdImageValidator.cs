namespace MyMarket.Web.Models.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using Common;

    public static class AdImageValidator
    {
        public static ValidationResult ValidateImage(HttpPostedFileBase image)
        {
            return Validate(image);
        }

        public static ValidationResult ValidateImages(IEnumerable<HttpPostedFileBase> images)
        {
            foreach (var img in images)
            {
                var invalidModelResult = Validate(img);

                if (invalidModelResult != null && !string.IsNullOrEmpty(invalidModelResult.ErrorMessage))
                {
                    return invalidModelResult;
                }    
            }

            return ValidationResult.Success;
        }

        private static ValidationResult Validate(HttpPostedFileBase image)
        {
            if (image != null && !IsImage(image))
            {
                var allowedTypes = ValidationConstants.JpgImage + ", " + ValidationConstants.PngImage + ", " + 
                    ValidationConstants.GifImage + ", " + ValidationConstants.JpegImage;
                return new ValidationResult(string.Format(ErrorMessages.InvalidImageType, allowedTypes));
            }

            if (image == null || (image != null && image.ContentLength <= ValidationConstants.AdImageContentLengthInBytes))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(string.Format(ErrorMessages.InvalidImageSize, ValidationConstants.AdImageContentLengthInKilobytes));
        }

        private static bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[]
            {
                ValidationConstants.JpgImage,
                ValidationConstants.PngImage, 
                ValidationConstants.GifImage,
                ValidationConstants.JpegImage
            };

            return formats.Any(x => file.FileName.EndsWith(x, StringComparison.OrdinalIgnoreCase));
        }
    }
}