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
            IQueryable<Item> result = db.Item;

            if (isadmin == "True")
            {
                result = result.OrderBy(x => x.Group);
            }
            else
            {
                IQueryable<int> groups = db.UserGroup.Where(x => x.User == account).Select(x => x.Group);

                result = result.Where(x => groups.Contains(x.Group));
            }

            return result.OrderBy(x => x.GroupDetail.Name).ToArray();
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
                //Item Limit
                string[] filenames = fileName.Split('.');
                long limit = 0;

                if (filenames.Length > 1)
                {
                    string ext = filenames[filenames.Length - 1].ToLower();

                    ItemLimit _limit = db.ItemLimit.FirstOrDefault(x => x.Name == ext);

                    if (_limit != null)
                    {
                        limit = TotalLimit(_limit.Limit, _limit.Size);

                        if (content.Length > limit) return _limit.Name + " file limit:  " + _limit.Limit + " " + _limit.Size;
                    }
                }

                long sum = 0;

                //Group Limint
                if (db.Item.Where(x => x.Group == item.Group && x.Idx != item.Idx).Count() != 0)
                    sum = db.Item.Where(x => x.Group == item.Group && x.Idx != item.Idx).Sum(x => x.Length);

                Group _group = db.Group.FirstOrDefault(x => x.Idx == item.Group);

                limit = TotalLimit(_group.Limit, _group.Size);

                if (sum + content.Length > limit) return "Not enough group space";

                //User Limit
                if (item.Creator == creator || string.IsNullOrWhiteSpace(item.Creator))
                {
                    sum = 0;

                    if (db.Item.Where(x => x.Creator == creator && x.Idx != item.Idx).Count() != 0)
                        sum = db.Item.Where(x => x.Creator == creator && x.Idx != item.Idx).Sum(x => x.Length);

                    User _user = db.User.FirstOrDefault(x => x.Account == creator);

                    limit = TotalLimit(_user.Limit, _user.Size);

                    if (sum + content.Length > limit) return "Not enough user space";
                }

                item.AccessTime = DateTime.Now;

                if (item.Idx == 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        content.CopyTo(ms);
                        item.Content = ms.GetBuffer();
                        item.Length = item.Content.Length;
                    }

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

        private long TotalLimit(int limit, string size)
        {
            long result = 0;

            switch (size)
            {
                case "Bytes":
                    result = limit;
                    break;
                case "KB":
                    result = limit * 1024;
                    break;
                case "MB":
                    result = limit * 1024 * 1024;
                    break;
                case "GB":
                    result = limit * 1024 * 1024 * 1024;
                    break;
            }

            return result;
        }

        public bool SaveLimit(ItemLimit _limit)
        {
            bool result = false;

            try
            {
                ItemLimit limit = db.ItemLimit.FirstOrDefault(x => x.Name == _limit.Name);

                if (limit == null)
                {
                    limit.Name = limit.Name.ToLower();

                    db.Entry(limit).State = EntityState.Added;
                }
                else
                {
                    limit.Limit = _limit.Limit;
                    limit.Size = _limit.Size;

                    db.Entry(limit).State = EntityState.Modified;
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