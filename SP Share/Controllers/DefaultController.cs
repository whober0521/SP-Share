using SP_Share.Services;
using SP_Share.Models;
using System.Web.Mvc;

namespace SP_Share.Controllers
{
    public class DefaultController : Controller
    {
        UserService userSrv;

        public DefaultController()
        {
            userSrv = new UserService();
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
                if (userSrv.Insert(model))
                    return RedirectToAction("Index");

            return View(model);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Account, string Password, string returnUrl)
        {
            User user = userSrv.Login(Account, Password);

            if (user != null)
            {
                Session.Clear();

                Session["IsAdmin"] = user.IsAdmin;
                Session["UserAccount"] = user.Name;
                Session["UserName"] = user.Name;
                Session["UserGroup"] = user.Group;

                return RedirectToLocal(returnUrl);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["IsAdmin"] = null;
            Session["UserAccount"] = null;
            Session["UserName"] = null;
            Session["UserGroup"] = null;

            Session.Clear();

            return RedirectToAction("Index", "Default");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Default");
        }
    }
}