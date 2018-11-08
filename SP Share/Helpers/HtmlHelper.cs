using System.Collections.Generic;
using System.Web.Mvc;
using SP_Share.Services;
using SP_Share.Models;

namespace SP_Share.Helpers
{
    public static class SPHtmlHelper
    {
        public static List<SelectListItem> GroupDropDownList(this HtmlHelper helper)
        {
            List<SelectListItem> result = new List<SelectListItem>() { new SelectListItem() { Text = "Select", Value = "" } };
            Group[] groupList = new GroupService().GetGroupList(false);

            foreach (Group group in groupList)
                result.Add(new SelectListItem() { Value = group.Idx.ToString(), Text = group.Name });

            return result;
        }

        public static List<SelectListItem> SizeDropDownList(this HtmlHelper helper)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            result.Add(new SelectListItem() { Value = "Bytes", Text = "Bytes" });
            result.Add(new SelectListItem() { Value = "KB", Text = "KB", Selected = true });
            result.Add(new SelectListItem() { Value = "MB", Text = "MB" });
            result.Add(new SelectListItem() { Value = "GB", Text = "GB" });

            return result;
        }
    }
}