using SP_Share.Models;
using System.Linq;

namespace SP_Share.Services
{
    public class ItemService : DefaultService
    {
        public ItemService()
        {

        }

        public Item[] GetItemList()
        {
            return db.Item.ToArray();
        }
    }
}