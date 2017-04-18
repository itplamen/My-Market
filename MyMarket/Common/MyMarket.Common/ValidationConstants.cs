namespace MyMarket.Common
{
    public static class ValidationConstants
    {
        // All entities
        public const int InvalidId = 0;
        public const int InvalidCount = 0;
        public const int NameMinLength = 2;
        public const int NameMaxLenght = 250;
        public const int ContentMinLength = 2;
        public const int ContentMaxLength = 4096;

        // Ads
        public const int AdImageContentLengthInKilobytes = 300;
        public const int AdImageContentLengthInBytes = AdImageContentLengthInKilobytes * 1024;
        public const string JpgImage = ".jpg";
        public const string JpegImage = ".jpeg";
        public const string PngImage = ".png";
        public const string GifImage = ".gif";

        // FileInfo
        public const int MaxOriginalFileNameLength = 255;
        public const int MaxFileExtensionLength = 4;
    }
}
