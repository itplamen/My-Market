namespace MyMarket.Common
{
    public static class ErrorMessages
    {
        // All entities
        public const string InvalidId = "ID is invalid!";
        public const string EntityCannotBeNull = "Entity cannot be null!";

        // Ad
        public const string InvalidImageSize = "The maximum size of the image is {0} kb!";
        public const string InvalidImageType = "The image type is invalid! Allowed types: {0}!";
    }
}