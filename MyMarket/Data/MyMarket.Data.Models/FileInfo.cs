namespace MyMarket.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common.Models;
    using MyMarket.Common;

    public abstract class FileInfo : BaseModel<int>
    {
        [Required]
        [MaxLength(ValidationConstants.MAX_ORIGINAL_FILE_NAME_LENGTH)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MAX_FILE_EXTENSION_LENGTH)]
        public string FileExtension { get; set; }

        public string UrlPath { get; set; }
    }
}
