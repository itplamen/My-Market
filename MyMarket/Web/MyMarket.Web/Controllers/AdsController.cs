namespace MyMarket.Web.Controllers
{
    using System.Web.Mvc;

    using Bytes2you.Validation;

    using Common;
    using Services.Data.Contracts;
    
    public class AdsController : BaseController
    {
        private readonly IAdsService adsService;

        public AdsController(IAdsService adsService)
        {
            Guard.WhenArgument(adsService, nameof(adsService)).IsNull().Throw();

            this.adsService = adsService;
        }

        public ActionResult Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();



            return this.View();
        }
    }
}