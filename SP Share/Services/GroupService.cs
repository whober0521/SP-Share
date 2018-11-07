using SP_Share.Models;
using System.Linq;

namespace SP_Share.Services
{
    public class GroupService : DefaultService
    {
        public GroupService()
        {
        }

        public Group[] GetGroupList()
        {
            return db.Group.Where(x => x.IsActive).ToArray();
        }
    }
}