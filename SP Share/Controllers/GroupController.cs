using System.Web.Mvc;

namespace SP_Share.Controllers
{
    public class GroupController : Controller
    {
        //ItemService itemSrv;

        public GroupController()
        {
            //itemSrv = new ItemService();
        }

        public ActionResult Group()
        {
            if (Session["UserAccount"] == null)
                return RedirectToAction("Index", "Default");

            return View();
        }

        //public ActionResult Index()
        //{
        //    if (Session["UserAccount"] == null)
        //        return RedirectToAction("Index", "Default");
        //    else
        //        return View(itemSrv.GetItemList(Session["UserAccount"].ToString(), Session["IsAdmin"].ToString()));
        //}



        //[ValidateInput(false)]
        //public ActionResult Insert(Item item, HttpPostedFileBase contentfile)
        //{
        //    if (Session["UserGroup"] == null)
        //        return RedirectToAction("Index", "Home");

        //    itemSrv.Insert(item, contentfile.InputStream, int.Parse(Session["UserGroup"].ToString()), Session["UserName"].ToString());

        //    return RedirectToAction("Index");
        //}

        //public FileResult Download(int? idx)
        //{
        //    Item item = itemSrv.GetItem((int)idx);

        //    return File(item.Content, System.Net.Mime.MediaTypeNames.Application.Octet, item.Name);
        //}

        //public ActionResult Delete(int? idx)
        //{
        //    itemSrv.Delete((int)idx);

        //    return RedirectToAction("Index");
        //}
    }
}