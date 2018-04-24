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
    using MyMarket.Web.Models.Comments;

    public class AdsController : BaseController
    {
        private readonly IAdsService adsService;
        private readonly ICategoriesService categoriesService;
        private readonly IFileSystemService fileSystemService;
        private readonly IImagesService imagesService;
        private readonly ICommentsService commentsService;

        public AdsController(
            IAdsService adsService,
            ICategoriesService categoriesService,
            IFileSystemService fileSystemService,
            IImagesService imagesService,
            ICommentsService commentsService)
        {
            Guard.WhenArgument(adsService, nameof(adsService)).IsNull().Throw();
            Guard.WhenArgument(categoriesService, nameof(categoriesService)).IsNull().Throw();
            Guard.WhenArgument(fileSystemService, nameof(fileSystemService)).IsNull().Throw();
            Guard.WhenArgument(imagesService, nameof(imagesService)).IsNull().Throw();
            Guard.WhenArgument(commentsService, nameof(commentsService)).IsNull().Throw();

            this.adsService = adsService;
            this.categoriesService = categoriesService;
            this.fileSystemService = fileSystemService;
            this.imagesService = imagesService;
            this.commentsService = commentsService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Post()
        {
            return this.View(new AdCreateViewModel()
            {
                Categories = this.GetCategories()
            });
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

            Image mainImage = this.ProcessImage(createModel.MainPicture);

            Ad adToCreate = Mapper.Map<Ad>(createModel);
            adToCreate.UserId = this.User.Identity.GetUserId();
            adToCreate.MainImageId = mainImage.Id;
            adToCreate.Images = this.ProcessImages(createModel.OtherPictures);

            int adId = this.adsService.Add(adToCreate);

            return RedirectToAction($"{adId}");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View(this.GetCategories());
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            if (id <= ValidationConstants.INVALID_ID)
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
            return this.Search(new AdsSearchViewModel(), NativeConstants.ADS_START_PAGE);
        }

        [HttpPost]
        [AjaxOnly]
        public PartialViewResult SearchAds(AdsSearchViewModel searchModel, int? page)
        {
            int actualPage = page ?? NativeConstants.ADS_START_PAGE;
            Guard.WhenArgument(actualPage, nameof(actualPage)).IsLessThan(NativeConstants.ADS_START_PAGE).Throw();

            return this.Search(searchModel, actualPage);
        }

        [HttpPost]
        [AjaxOnly]
        public PartialViewResult AddComment(string content, int adId)
        {
            var comment = new Comment()
            {
                Content = content,
                AdId = adId,
                UserId = this.User.Identity.GetUserId()
            };

            this.commentsService.Add(comment);

            var viewModel = Mapper.Map<CommentViewModel>(comment);
            viewModel.Username = this.User.Identity.Name;

            return this.PartialView("_CommentPartial", viewModel);
        }

        [HttpPost]
        [AjaxOnly]
        public PartialViewResult DeleteComment(int id)
        {
            this.commentsService.Delete(id);

            return this.PartialView("_CommentPartial", new CommentViewModel());
        }

        private PartialViewResult Search(AdsSearchViewModel search, int page)
        {
            Guard.WhenArgument(page, nameof(page)).IsLessThan(NativeConstants.ADS_START_PAGE).Throw();

            int allAdsCount = this.adsService.All().Count();
            int totalPages = (int)Math.Ceiling(allAdsCount / (decimal)NativeConstants.AdsPerPage);

            var ads = this.adsService
                .Search(search.SearchWord, search.ChosenCategoriesIds, search.SortBy, search.SortType, page)
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

        private ICollection<Image> ProcessImages(IEnumerable<HttpPostedFileBase> images)
        {
            ICollection<Image> result = new List<Image>();

            foreach (var image in images)
            {
                if (image != null)
                {
                    Image savedImage = this.ProcessImage(image);
                    result.Add(savedImage);
                }
            }

            return result;
        }

        private Image ProcessImage(HttpPostedFileBase image)
        {
            string path = this.Server.MapPath(NativeConstants.IMAGES_PATH);
            string urlPath = this.fileSystemService.SaveImage(image, path);

            var imageToSave = new Image()
            {
                FileExtension = Path.GetExtension(image.FileName),
                OriginalFileName = image.FileName,
                UrlPath = urlPath
            };

            this.imagesService.Add(imageToSave);

            return imageToSave;
        }
    }
}