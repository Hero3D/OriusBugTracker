using System.Web.Mvc;

namespace Orius.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult AdminIndex()
        {
            return View();
        }
    }
}