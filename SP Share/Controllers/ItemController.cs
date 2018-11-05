using SP_Share.Services;
using System.Web.Mvc;

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
            return View(itemSrv.GetItemList());
        }
    }
}