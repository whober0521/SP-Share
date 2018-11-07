using SP_Share.Services;
using System.Web.Mvc;

namespace SP_Share.Controllers
{
    public class GroupController : Controller
    {
        GroupService groupSrv;

        public GroupController()
        {
            groupSrv = new GroupService();
        }

        public ActionResult Group()
        {
            if (Session["UserAccount"] == null)
                return RedirectToAction("Index", "Default");

            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Insert(string Name)
        {
            if (Session["UserAccount"] == null)
                return RedirectToAction("Index", "Home");

            groupSrv.Insert(Name);

            return RedirectToAction("Group");
        }


        //public ActionResult Index()
        //{
        //    if (Session["UserAccount"] == null)
        //        return RedirectToAction("Index", "Default");
        //    else
        //        return View(itemSrv.GetItemList(Session["UserAccount"].ToString(), Session["IsAdmin"].ToString()));
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