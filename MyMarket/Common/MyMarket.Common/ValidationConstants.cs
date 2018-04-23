namespace MyMarket.Common
{
    public static class ValidationConstants
    {
        // All entities
        public const int INVALID_ID = 0;
        public const int INVALID_COUNT = 0;
        public const int NAME_MIN_LENGTH = 2;
        public const int NAME_MAX_LENGTH = 250;
        public const int CONTENT_MIN_LENGTH = 2;
        public const int CONTENT_MAX_LENGTH = 66664096;

        // Ads
        public const int AD_IMAGE_CONTENT_LENGTH_IN_KILOBYTES = 300;
        public const int AD_IMAGE_CONTENT_LENGTH_IN_BYTES = AD_IMAGE_CONTENT_LENGTH_IN_KILOBYTES * 1024;
        public const string JPG_IMAGE = ".jpg";
        public const string JPEG_IMAGE = ".jpeg";
        public const string PNG_IMAGE = ".png";
        public const string GIF_IMAGE = ".gif";

        // FileInfo
        public const int MAX_ORIGINAL_FILE_NAME_LENGTH = 255;
        public const int MAX_FILE_EXTENSION_LENGTH = 4;
    }
}