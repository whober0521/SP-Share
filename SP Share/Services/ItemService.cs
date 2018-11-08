using System.Data.Entity;
using System.Linq;
using System.IO;
using System;
using SP_Share.Models;

namespace SP_Share.Services
{
    public class ItemService : DefaultService
    {
        const int KBtye = 1024;

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

        public ItemLimit[] GetItemLimitList()
        {
            return db.ItemLimit.ToArray();
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

        public ItemLimit GetItemLimit(string name)
        {
            ItemLimit result = db.ItemLimit.FirstOrDefault(x => x.Name == name);

            if (result == null) result = new ItemLimit();

            return result;
        }

        public string Save(Item item, Stream content, string fileName, string creator)
        {
            string result = "";

            try
            {
                //Group Limint
                int sum = 0;

                if (db.Item.Where(x => x.Group == item.Group).Count() != 0)
                    sum = db.Item.Where(x => x.Group == item.Group && x.Idx != item.Idx).Sum(x => x.Length);

                if (sum + content.Length > db.Group.FirstOrDefault(x => x.Idx == item.Group).Limit * 1024 * 1024) return "Not enough group space";

                //User Limit
                sum = 0;

                if (db.Item.Where(x => x.Creator == creator).Count() != 0)
                    sum = db.Item.Where(x => x.Creator == creator && x.Idx != item.Idx).Sum(x => x.Length);

                if (sum + content.Length > db.User.FirstOrDefault(x => x.Account == creator).Limit * 1024 * 1024) return "Not enough user space";

                //Item Limit
                string[] filenames = fileName.Split('.');

                if (filenames.Length > 1)
                {
                    string ext = filenames[filenames.Length - 1];

                    ItemLimit limit = db.ItemLimit.FirstOrDefault(x => x.Name == ext);

                    if (limit != null)
                    {
                        if (content.Length > limit.Limit * 1024 * 1024)
                            return "Item Limit  " + limit.Limit + " MB";
                    }
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    content.CopyTo(ms);
                    item.Content = ms.GetBuffer();
                    item.Length = item.Content.Length;
                }

                item.AccessTime = DateTime.Now;

                if (item.Idx == 0)
                {
                    item.Creator = creator;
                    item.CreateTime = DateTime.Now;

                    db.Entry(item).State = EntityState.Added;

                    UserGroup ug = db.UserGroup.FirstOrDefault(x => x.User == creator && x.Group == item.Group);

                    if (ug == null)
                    {
                        ug = new UserGroup();

                        ug.User = creator;
                        ug.Group = item.Group;

                        db.Entry(ug).State = EntityState.Added;
                    }
                }
                else
                {
                    db.Entry(item).State = EntityState.Modified;
                }

                db.SaveChanges();

                result = "";
            }
            catch (Exception)
            {
                result = "";
            }

            return result;
        }

        public bool SaveLimit(ItemLimit limit)
        {
            bool result = false;

            try
            {
                if (db.ItemLimit.FirstOrDefault(x => x.Name == limit.Name) == null)
                    db.Entry(limit).State = EntityState.Added;
                else
                    db.Entry(limit).State = EntityState.Modified;

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

        public bool DeleteLimit(string name)
        {
            bool result = false;

            try
            {
                ItemLimit item = db.ItemLimit.FirstOrDefault(x => x.Name == name);

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