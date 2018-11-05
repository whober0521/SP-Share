using System.Web.Mvc;

namespace SP_Share.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = " CSE 5382 Secure Programming Fall 2018 Assignment:  SP Share";

            return View();
        }
    }
}