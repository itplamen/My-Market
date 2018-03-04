namespace MyMarket.Common.Validators
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
            foreach (var image in images)
            {
                ValidationResult validationResult = Validate(image);

                if (validationResult != null && !string.IsNullOrEmpty(validationResult.ErrorMessage))
                {
                    return validationResult;
                }
            }

            return ValidationResult.Success;
        }

        private static ValidationResult Validate(HttpPostedFileBase image)
        {
            if (image != null && !IsImage(image))
            {
                string allowedTypes = ValidationConstants.JPG_IMAGE + ", " + ValidationConstants.PNG_IMAGE + ", " +
                    ValidationConstants.GIF_IMAGE + ", " + ValidationConstants.JPEG_IMAGE;

                return new ValidationResult(string.Format(ErrorMessages.INVALID_IMAGE_TYPE, allowedTypes));
            }

            if (image == null || (image != null && image.ContentLength <= ValidationConstants.AD_IMAGE_CONTENT_LENGTH_IN_BYTES))
            {
                return ValidationResult.Success;
            }

            string errorMessage = string.Format(ErrorMessages.INVALID_IMAGE_SIZE, ValidationConstants.AD_IMAGE_CONTENT_LENGTH_IN_KILOBYTES);
            return new ValidationResult(errorMessage);
        }

        private static bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[]
            {
                ValidationConstants.JPG_IMAGE,
                ValidationConstants.PNG_IMAGE,
                ValidationConstants.GIF_IMAGE,
                ValidationConstants.JPEG_IMAGE
            };

            return formats.Any(x => file.FileName.EndsWith(x, StringComparison.OrdinalIgnoreCase));
        }
    }
}