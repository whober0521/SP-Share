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

        public Item[] GetItemList(string account, string isadmin)
        {
            Item[] result;

            if (isadmin == "True")
                result = db.Item.OrderBy(x => x.Group).ToArray();
            else
                result = db.Item.Where(x => x.Creator == account).OrderBy(x => x.Group).ToArray();

            return result;
        }

        public Item GetItem(int? idx)
        {
            Item item = new Item();

            try
            {
                item = db.Item.FirstOrDefault(x => x.Idx == idx);

                if (item != null)
                {
                    item.AccessTime = DateTime.Now;

                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

            return item;
        }

        public bool Save(Item item, Stream content, string creator)
        {
            bool result = false;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    content.CopyTo(ms);
                    item.Content = ms.GetBuffer();
                }

                item.AccessTime = DateTime.Now;

                if (item.Idx == 0)
                {
                    item.Creator = creator;
                    item.CreateTime = DateTime.Now;

                    db.Entry(item).State = EntityState.Added;
                }
                else
                {
                    db.Entry(item).State = EntityState.Modified;
                }

                db.SaveChanges();

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool Delete(int? idx)
        {
            bool result = false;

            try
            {
                Item item = db.Item.FirstOrDefault(x => x.Idx == idx);

                if (item != null)
                {
                    db.Entry(item).State = EntityState.Deleted;
                    db.SaveChanges();
                }

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}