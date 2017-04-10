namespace MyMarket.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Common;
    using Contracts;
    using MyMarket.Data.Common;
    using MyMarket.Data.Models;

    public class AdsService : IAdsService
    {
        private readonly IDbRepository<Ad> adsRepository;

        public AdsService(IDbRepository<Ad> adsRepository)
        {
            this.adsRepository = adsRepository;
        }

        public int Add(Ad ad)
        {
            Guard.WhenArgument(ad, nameof(ad)).IsNull().Throw();

            this.adsRepository.Add(ad);
            this.adsRepository.Save();

            return ad.Id;
        }

        public Ad Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();

            return this.adsRepository.GetById(id);
        }

        public IQueryable<Ad> All()
        {
            return this.adsRepository.All();
        }

        public IQueryable<Ad> AllWithDeleted()
        {
            return this.adsRepository.AllWithDeleted();
        }

        public Ad Update(int id, Ad ad)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();
            Guard.WhenArgument(ad, nameof(ad)).IsNull().Throw();

            var adToUpdate = this.adsRepository.GetById(id);

            if (adToUpdate != null)
            {
                adToUpdate.CreatedOn = ad.CreatedOn;
                adToUpdate.IsDeleted = ad.IsDeleted;
                adToUpdate.DeletedOn = ad.DeletedOn;
                adToUpdate.Title = ad.Title;
                adToUpdate.Description = ad.Description;
                adToUpdate.Picture = ad.Picture;
                adToUpdate.Price = ad.Price;
                adToUpdate.CategoryId = ad.CategoryId;

                this.adsRepository.Save();
            }

            return adToUpdate;
        }

        public Ad Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();

            var adToDelete = this.adsRepository.GetById(id);

            if (adToDelete != null)
            {
                this.adsRepository.Delete(adToDelete);
                this.adsRepository.Save();
            }

            return adToDelete;
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();

            var adToDelete = this.adsRepository.GetById(id);

            if (adToDelete != null)
            {
                this.adsRepository.HardDelete(adToDelete);
                this.adsRepository.Save();

                return true;
            }

            return false;
        }
    }
}
