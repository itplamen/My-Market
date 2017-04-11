namespace MyMarket.Web.Controllers
{
    using System.Web.Mvc;

    using Bytes2you.Validation;

    using Services.Data.Contracts;

    public class HomeController : BaseController
    {
        private readonly IAdsService adsService;

        public HomeController(IAdsService adsService)
        {
            Guard.WhenArgument(adsService, nameof(adsService)).IsNull().Throw();

            this.adsService = adsService;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}