namespace MyMarket.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Bytes2you.Validation;

    using Models.Home;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;

    public class HomeController : BaseController
    {
        private readonly IAdsService adsService;

        public HomeController(IAdsService adsService)
        {
            Guard.WhenArgument(adsService, nameof(adsService)).IsNull().Throw();

            this.adsService = adsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var latestAds = this.Cache.Get(
                "latestAds",
                () => this.adsService.Latest().To<AdViewModel>().ToList(),
                15 * 60);

            var mostLikedAds = this.Cache.Get(
                "mostLikedAds",
                () => this.adsService.MostLiked().To<AdViewModel>().ToList(),
                15 * 60);

            var selectedAds = new SelectedAdsViewModel();
            selectedAds.LatestAds = latestAds;
            selectedAds.MostLiked = mostLikedAds;

            return View(selectedAds);
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}