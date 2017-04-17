namespace MyMarket.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using Bytes2you.Validation;

    using Microsoft.AspNet.Identity;

    using Attributes;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using Models.Ad;
    using Models.Category;
    using Services.Data.Contracts;

    public class AdsController : BaseController
    {
        private readonly IAdsService adsService;
        private readonly ICategoriesService categoriesService;
        private readonly IImagesService imagesService;

        public AdsController(IAdsService adsService, ICategoriesService categoriesService, IImagesService imagesService)
        {
            Guard.WhenArgument(adsService, nameof(adsService)).IsNull().Throw();
            Guard.WhenArgument(categoriesService, nameof(categoriesService)).IsNull().Throw();
            Guard.WhenArgument(imagesService, nameof(imagesService)).IsNull().Throw();

            this.adsService = adsService;
            this.categoriesService = categoriesService;
            this.imagesService = imagesService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Post()
        {
            return this.View(new AdCreateViewModel() { Categories = this.GetCategories() });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Post(AdCreateViewModel createModel)
        {
            if (!this.ModelState.IsValid)
            {
                createModel.Categories = this.GetCategories();

                return this.View(createModel);
            }

            var adToCreate = Mapper.Map<Ad>(createModel);
            adToCreate.UserId = this.User.Identity.GetUserId();

            this.adsService.Add(adToCreate);

            this.ProcessImages(createModel.MainPicture, createModel.OtherPictures, adToCreate.Id);

            // TODO: Redirect to Ads/{id}
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View(this.GetCategories());
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
            return this.Search(new AdsSearchViewModel(), Common.Constants.AdsStartPage);
        }

        [HttpPost]
        [AjaxOnly]
        public PartialViewResult SearchAds(AdsSearchViewModel searchModel, int? page)
        {
            int actualPage = page ?? Common.Constants.AdsStartPage;
            Guard.WhenArgument(actualPage, nameof(actualPage)).IsLessThan(Common.Constants.AdsStartPage).Throw();

            return this.Search(searchModel, actualPage);
        }

        private PartialViewResult Search(AdsSearchViewModel search, int page)
        {
            Guard.WhenArgument(page, nameof(page)).IsLessThan(Common.Constants.AdsStartPage).Throw();

            var allAdsCount = this.adsService.All().Count();
            var totalPages = (int)Math.Ceiling(allAdsCount / (decimal)Common.Constants.AdsPerPage);

            var ads = this.adsService.Search(search.SearchWord, search.ChosenCategoriesIds, search.SortBy, search.SortType, page)
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

        private IEnumerable<CategoryViewModel> GetCategories()
        {
            return this.Cache.Get(
                "categories",
                () => this.categoriesService.All()
                    .OrderBy(c => c.Name)
                    .To<CategoryViewModel>()
                    .ToList(),
                15 * 60);
        }

        private void ProcessImages(HttpPostedFileBase mainImage, IEnumerable<HttpPostedFileBase> otherImages, int adId)
        {
            if (mainImage != null)
            {
                this.UploadImage(mainImage, adId);
            }

            if (otherImages != null && otherImages.Any())
            {
                foreach (var img in otherImages)
                {
                    if (img != null)
                    {
                        this.UploadImage(img, adId);
                    }
                }
            }
        }

        private void UploadImage(HttpPostedFileBase image, int adId)
        {
            string fileExtension = Path.GetExtension(image.FileName);
            string fileName = string.Format("{0}.{1}", Guid.NewGuid(), fileExtension);
            string path = Path.Combine(this.Server.MapPath(@"~/Content/Images/Ads/"), fileName);
            image.SaveAs(path);

            this.SaveImage(fileExtension, image.FileName, path, adId);
        }

        private void SaveImage(string fileExtension, string originalFileName, string urlPath, int adId)
        {
            var image = new Image()
            {
                FileExtension = fileExtension,
                OriginalFileName = originalFileName,
                UrlPath = urlPath,
                AdId = adId
            };

            this.imagesService.Add(image);
        }
    }
}