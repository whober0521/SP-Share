using System.Web.Mvc;

namespace SP_Share.Controllers
{
    public class DefaultController : Controller
    {
        public DefaultController()
        {
        }

        public ActionResult Index()
        {
            if (Session["UserAccount"] == null)
                return View();
            else
                return RedirectToAction("Index", "Item");
        }
    }
}