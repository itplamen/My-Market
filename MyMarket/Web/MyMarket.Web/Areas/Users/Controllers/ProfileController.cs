using System.Web.Mvc;

namespace MyMarket.Web.Areas.Users.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}