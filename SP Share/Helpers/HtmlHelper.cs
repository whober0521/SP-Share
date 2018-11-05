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
            Group[] groupList = new UserService().GetGroupList();

            foreach (Group group in groupList)
                result.Add(new SelectListItem() { Value = group.Idx.ToString(), Text = group.Name });

            return result;
        }
    }
}