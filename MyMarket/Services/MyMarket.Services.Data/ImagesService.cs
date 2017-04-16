namespace MyMarket.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Common;
    using Contracts;
    using MyMarket.Data.Common;
    using MyMarket.Data.Models;

    public class ImagesService : IImagesService
    {
        private readonly IDbRepository<Image> imagesRepository;

        public ImagesService(IDbRepository<Image> imagesRepository)
        {
            Guard.WhenArgument(imagesRepository, nameof(imagesRepository)).IsNull().Throw();
        }

        public int Add(Image image)
        {
            Guard.WhenArgument(image, nameof(image)).IsNull().Throw();

            this.imagesRepository.Add(image);
            this.imagesRepository.Save();

            return image.Id;
        }

        public Image Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.imagesRepository.GetById(id);
        }

        public IQueryable<Image> GetAsQueryable(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.imagesRepository.All()
                .Where(i => i.Id == id);
        }

        public IQueryable<Image> All()
        {
            return this.imagesRepository.All();
        }

        public IQueryable<Image> AllWithDeleted()
        {
            return this.imagesRepository.AllWithDeleted();
        }

        public Image Update(int id, Image image)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();
            Guard.WhenArgument(image, nameof(image)).IsNull().Throw();

            var imageToUpdate = this.imagesRepository.GetById(id);

            if (imageToUpdate != null)
            {
                imageToUpdate.CreatedOn = image.CreatedOn;
                imageToUpdate.IsDeleted = image.IsDeleted;
                imageToUpdate.DeletedOn = image.DeletedOn;
                imageToUpdate.AdId = image.AdId;
                imageToUpdate.OriginalFileName = image.OriginalFileName;
                imageToUpdate.FileExtension = image.FileExtension;
                imageToUpdate.UrlPath = image.UrlPath;

                this.imagesRepository.Save();
            }

            return imageToUpdate;
        }

        public Image Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var imageToDelete = this.imagesRepository.GetById(id);

            if (imageToDelete != null)
            {
                this.imagesRepository.Delete(imageToDelete);
                this.imagesRepository.Save();
            }

            return imageToDelete;
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var imageToDelete = this.imagesRepository.GetById(id);

            if (imageToDelete != null)
            {
                this.imagesRepository.HardDelete(imageToDelete);
                this.imagesRepository.Save();

                return true;
            }

            return false;
        }
    }
}
