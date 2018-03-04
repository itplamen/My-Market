namespace MyMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic;

    using Bytes2you.Validation;

    using Common;
    using Contracts;
    using MyMarket.Data.Common;
    using MyMarket.Data.Models;

    public class AdsService : IAdsService
    {
        private const int MinAdsPerPageCount = 1;
        private readonly IDbRepository<Ad> adsRepository;

        public AdsService(IDbRepository<Ad> adsRepository)
        {
            Guard.WhenArgument(adsRepository, nameof(adsRepository)).IsNull().Throw();

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

        public IQueryable<Ad> GetAsQueryable(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();

            return this.adsRepository.All()
                .Where(a => a.Id == id);
        }

        public IQueryable<Ad> All()
        {
            return this.adsRepository.All();
        }

        public IQueryable<Ad> AllWithDeleted()
        {
            return this.adsRepository.AllWithDeleted();
        }

        public IQueryable<Ad> Latest(int count = NativeConstants.TopAdsCount)
        {
            Guard.WhenArgument(count, nameof(count)).IsLessThanOrEqual(ValidationConstants.INVALID_COUNT).Throw();

            return this.adsRepository.All()
                .OrderByDescending(a => a.CreatedOn)
                .Take(count);
        }

        public IQueryable<Ad> MostLiked(int count = NativeConstants.TopAdsCount)
        {
            Guard.WhenArgument(count, nameof(count)).IsLessThanOrEqual(ValidationConstants.INVALID_COUNT).Throw();

            return this.adsRepository.All()
                .OrderByDescending(a => a.Likes.Count)
                .Take(count);
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
                adToUpdate.MainImageId = ad.MainImageId;
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

        public IQueryable<Ad> Search(
            string searchWord, 
            IEnumerable<int> categoriesIds, 
            string sortBy, 
            string sortType, 
            int page = NativeConstants.ADS_START_PAGE, 
            int adsPerPage = NativeConstants.AdsPerPage)
        {
            Guard.WhenArgument(sortBy, nameof(sortBy)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(sortType, nameof(sortType)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(page, nameof(page)).IsLessThan(NativeConstants.ADS_START_PAGE).Throw();
            Guard.WhenArgument(adsPerPage, nameof(adsPerPage)).IsLessThanOrEqual(MinAdsPerPageCount).Throw();

            var adsToSkip = (page - 1) * NativeConstants.AdsPerPage;
 
            var ads = this.BuildFilterQuery(searchWord, categoriesIds)
                .OrderBy(sortBy + " " + sortType)
                .Skip(adsToSkip)
                .Take(adsPerPage);

            return ads;
        }

        private IQueryable<Ad> BuildFilterQuery(string searchWord, IEnumerable<int> categoriesIds)
        {
            var ads = this.adsRepository.All();

            if (!string.IsNullOrEmpty(searchWord))
            {
                ads = ads.Where(a => a.Title.Contains(searchWord) || a.User.Email.Contains(searchWord) || 
                    a.User.UserName.Contains(searchWord) || a.User.UserSettings.FirstName == searchWord || 
                    a.User.UserSettings.LastName == searchWord);
            }

            if (categoriesIds != null && categoriesIds.Any())
            {
                ads = ads.Where(a => categoriesIds.Contains(a.CategoryId));
            }

            return ads;
        }
    }
}