using System.Data.Entity;
using System.Linq;
using System.IO;
using System;
using SP_Share.Models;

namespace SP_Share.Services
{
    public class ItemService : DefaultService
    {
        public ItemService()
        {

        }

        public Item[] GetItemList(int group)
        {
            return db.Item.Where(x => x.Group == group).ToArray();
        }

        public Item GetItem(int idx)
        {
            return db.Item.FirstOrDefault();
        }

        public bool Insert(Item item, Stream content, int group, string creator)
        {
            bool result = false;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    content.CopyTo(ms);
                    item.Content = ms.GetBuffer();
                }

                item.Group = group;
                item.Creator = creator;
                item.CreateTime = DateTime.Now;
                item.AccessTime = DateTime.Now;

                db.Entry(item).State = EntityState.Added;
                db.SaveChanges();

                result = true;
            }
            catch (Exception)
            {

            }

            return result;
        }
    }
}