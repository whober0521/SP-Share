using SP_Share.Services;
using SP_Share.Models;
using System.Web.Mvc;
using System.Web;

namespace SP_Share.Controllers
{
    public class ItemController : Controller
    {
        ItemService itemSrv;

        public ItemController()
        {
            itemSrv = new ItemService();
        }

        public ActionResult Index()
        {
            if (Session["UserGroup"] == null)
                return RedirectToAction("Index", "Home");

            return View(itemSrv.GetItemList(int.Parse(Session["UserGroup"].ToString())));
        }

        public ActionResult Put()
        {
            if (Session["UserGroup"] == null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [ValidateInput(false)]
        public ActionResult Insert(Item item, HttpPostedFileBase contentfile)
        {
            if (Session["UserGroup"] == null)
                return RedirectToAction("Index", "Home");

            itemSrv.Insert(item, contentfile.InputStream, int.Parse(Session["UserGroup"].ToString()), Session["UserName"].ToString());

            return RedirectToAction("Index");
        }
    }
}