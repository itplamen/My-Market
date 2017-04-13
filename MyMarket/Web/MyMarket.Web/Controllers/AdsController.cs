namespace MyMarket.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Bytes2you.Validation;

    using Common;
    using Models.Ad;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;

    public class AdsController : BaseController
    {
        private readonly IAdsService adsService;

        public AdsController(IAdsService adsService)
        {
            Guard.WhenArgument(adsService, nameof(adsService)).IsNull().Throw();

            this.adsService = adsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var allAds = this.adsService.All()
                .To<AdViewModel>()
                .ToList();

            return this.View(allAds);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            if (id <= ValidationConstants.INVALID_ID)
            {
                return this.View("AdError");
            }

            var ad = this.adsService.GetAsQueryable(id)
                .To<AdViewModel>()
                .FirstOrDefault();

            if (ad == null)
            {
                return this.View("AdError");
            }

            return this.View(ad);
        }
    }
}