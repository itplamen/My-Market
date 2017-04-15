namespace MyMarket.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Bytes2you.Validation;

    using Attributes;
    using Common;
    using Infrastructure.Mapping;
    using Models.Ad;
    using Models.Category;
    using Services.Data.Contracts;

    public class AdsController : BaseController
    {
        private readonly IAdsService adsService;
        private readonly ICategoriesService categoriesService;

        public AdsController(IAdsService adsService, ICategoriesService categoriesService)
        {
            Guard.WhenArgument(adsService, nameof(adsService)).IsNull().Throw();
            Guard.WhenArgument(categoriesService, nameof(categoriesService)).IsNull().Throw();

            this.adsService = adsService;
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var categories = this.categoriesService.All()
                .To<CategoryViewModel>()
                .ToList();

            return this.View(categories);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            if (id <= ValidationConstants.InvalidId)
            {
                return this.View("AdError");
            }

            var adById = this.adsService.GetAsQueryable(id)
                .To<AdViewModel>()
                .FirstOrDefault();

            if (adById == null)
            {
                return this.View("AdError");
            }

            return this.View(adById);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult InitialAds()
        {
            return this.Search(new AdsSearchViewModel(), Constants.AdsStartPage);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult InitialAdsAjax(AdsSearchViewModel ajaxSearchModel)
        {
            return this.Search(ajaxSearchModel, Constants.AdsStartPage);
        }

        [HttpPost]
        [AjaxOnly]
        public PartialViewResult SearchAds(AdsSearchViewModel searchModel, int? page)
        {
            int actualPage = page ?? Constants.AdsStartPage;
            Guard.WhenArgument(actualPage, nameof(actualPage)).IsLessThan(Constants.AdsStartPage).Throw();

            return this.Search(searchModel, actualPage);
        }

        private PartialViewResult Search(AdsSearchViewModel searchModel, int page)
        {
            Guard.WhenArgument(page, nameof(page)).IsLessThan(Constants.AdsStartPage).Throw();

            var allAdsCount = this.adsService.All().Count();
            var totalPages = (int)Math.Ceiling(allAdsCount / (decimal)Constants.AdsPerPage);

            var ads = this.adsService.Search(
                searchModel.SearchWord, 
                searchModel.ChosenCategoriesIds, 
                searchModel.SortBy, 
                searchModel.SortType, 
                page)
                .To<AdViewModel>()
                .ToList();

            var adsListViewModel = new AdsSearchResultViewModel()
            {
                Ads = ads,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return this.PartialView("_AdsPartial", adsListViewModel);
        }
    }
}