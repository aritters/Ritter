using System.Web.Mvc;

namespace Aritter.Web.Controllers
{
    public class HomeController : DefaultController
    {
        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        #endregion Methods
    }
}