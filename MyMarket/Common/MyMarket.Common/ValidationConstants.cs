namespace MyMarket.Common
{
    public static class ValidationConstants
    {
        // All entities
        public const int InvalidId = 0;
        public const int InvalidCount = 0;
        public const int NameMinLength = 2;
        public const int NameMaxLenght = 50;
        public const int ContentMinLength = 2;
        public const int ContentMaxLength = 4096;

        // FileInfo
        public const int MaxOriginalFileNameLength = 255;
        public const int MaxFileExtensionLength = 4;
    }
}
