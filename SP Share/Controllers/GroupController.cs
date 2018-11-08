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

        public ActionResult Index()
        {
            if (Session["IsAdmin"] == null || Session["IsAdmin"].ToString() != "True")
                return RedirectToAction("Index", "Default");
            else
                return View(groupSrv.GetGroupList(true));
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

            groupSrv.Insert(Name, Session["IsAdmin"].ToString());

            return RedirectToAction("Group");
        }

        public ActionResult Active(int? idx)
        {
            if (Session["IsAdmin"] == null || Session["IsAdmin"].ToString() != "True")
                return RedirectToAction("Index", "Default");

            if (idx != null) groupSrv.Active(idx);

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Limit(int? Idx, int? Limit, string size)
        {
            if (Session["IsAdmin"] == null || Session["IsAdmin"].ToString() != "True")
                return RedirectToAction("Index", "Default");

            groupSrv.Limit(Idx, Limit, size);

            return RedirectToAction("Index");
        }

        public PartialViewResult _Limit(int? idx)
        {
            return PartialView(groupSrv.GetGroup(idx));
        }
    }
}