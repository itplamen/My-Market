namespace MyMarket.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Common;
    using Contracts;
    using MyMarket.Data.Common;
    using MyMarket.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDbRepository<Category> categoriesRepository;

        public CategoriesService(IDbRepository<Category> categoriesRepository)
        {
            Guard.WhenArgument(categoriesRepository, nameof(categoriesRepository)).IsNull().Throw();

            this.categoriesRepository = categoriesRepository;
        }

        public int Add(Category category)
        {
            Guard.WhenArgument(category, nameof(category)).IsNull().Throw();

            this.categoriesRepository.Add(category);
            this.categoriesRepository.Save();

            return category.Id;
        }

        public Category Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.categoriesRepository.GetById(id);
        }

        public IQueryable<Category> GetAsQueryable(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            return this.categoriesRepository.All().Where(c => c.Id == id);
        }

        public IQueryable<Category> All()
        {
            return this.categoriesRepository.All();
        }

        public IQueryable<Category> AllWithDeleted()
        {
            return this.categoriesRepository.AllWithDeleted();
        }

        public Category Update(int id, Category category)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();
            Guard.WhenArgument(category, nameof(category)).IsNull().Throw();

            var categoryToUpdate = this.categoriesRepository.GetById(id);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.CreatedOn = category.CreatedOn;
                categoryToUpdate.IsDeleted = category.IsDeleted;
                categoryToUpdate.DeletedOn = category.DeletedOn;
                categoryToUpdate.Name = category.Name;

                this.categoriesRepository.Save();
            }

            return categoryToUpdate;
        }

        public Category Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var categoryToDelete = this.categoriesRepository.GetById(id);

            if (categoryToDelete != null)
            {
                this.categoriesRepository.Delete(categoryToDelete);
                this.categoriesRepository.Save();
            }

            return categoryToDelete;
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.InvalidId).Throw();

            var categoryToDelete = this.categoriesRepository.GetById(id);

            if (categoryToDelete != null)
            {
                this.categoriesRepository.HardDelete(categoryToDelete);
                this.categoriesRepository.Save();

                return true;
            }

            return false;
        }
    }
}
