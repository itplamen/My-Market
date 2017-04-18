namespace MyMarket.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
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
    using Services.FileSystem.Contracts;

    public class AdsController : BaseController
    {
        private readonly IAdsService adsService;
        private readonly ICategoriesService categoriesService;
        private readonly IFileSystemService fileSystemService;
        private readonly IImagesService imagesService;

        public AdsController(IAdsService adsService, ICategoriesService categoriesService, IFileSystemService fileSystemService, IImagesService imagesService)
        {
            Guard.WhenArgument(adsService, nameof(adsService)).IsNull().Throw();
            Guard.WhenArgument(categoriesService, nameof(categoriesService)).IsNull().Throw();
            Guard.WhenArgument(fileSystemService, nameof(fileSystemService)).IsNull().Throw();
            Guard.WhenArgument(imagesService, nameof(imagesService)).IsNull().Throw();

            this.adsService = adsService;
            this.categoriesService = categoriesService;
            this.fileSystemService = fileSystemService;
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

            var imagesPath = this.Server.MapPath(NativeConstants.ImagesPath);

            if (createModel.MainPicture != null)
            {
                var savedImage = this.ProcessImage(createModel.MainPicture, imagesPath);
                adToCreate.MainImageId = savedImage.Id;
            }

            if (createModel.OtherPictures != null && createModel.OtherPictures.Any())
            {
                foreach (var img in createModel.OtherPictures)
                {
                    var savedImage = this.ProcessImage(createModel.MainPicture, imagesPath);
                    adToCreate.Images.Add(savedImage);
                }
            }
            
            this.adsService.Add(adToCreate);

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
            return this.Search(new AdsSearchViewModel(), NativeConstants.AdsStartPage);
        }

        [HttpPost]
        [AjaxOnly]
        public PartialViewResult SearchAds(AdsSearchViewModel searchModel, int? page)
        {
            int actualPage = page ?? NativeConstants.AdsStartPage;
            Guard.WhenArgument(actualPage, nameof(actualPage)).IsLessThan(NativeConstants.AdsStartPage).Throw();

            return this.Search(searchModel, actualPage);
        }

        private PartialViewResult Search(AdsSearchViewModel search, int page)
        {
            Guard.WhenArgument(page, nameof(page)).IsLessThan(NativeConstants.AdsStartPage).Throw();

            var allAdsCount = this.adsService.All().Count();
            var totalPages = (int)Math.Ceiling(allAdsCount / (decimal)NativeConstants.AdsPerPage);

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

        private Image ProcessImage(HttpPostedFileBase image, string imagesPath)
        {
            var urlPath = this.fileSystemService.SaveImage(image, imagesPath);
            var fileName = image.FileName;

            return this.SaveImage(Path.GetExtension(fileName), fileName, urlPath);
        }

        private Image SaveImage(string fileExtension, string originalFileName, string urlPath)
        {
            var image = new Image()
            {
                FileExtension = fileExtension,
                OriginalFileName = originalFileName,
                UrlPath = urlPath
            };

            this.imagesService.Add(image);

            return image;
        }
    }
}