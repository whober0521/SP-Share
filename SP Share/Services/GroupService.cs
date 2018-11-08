using System.Data.Entity;
using System.Linq;
using System;
using SP_Share.Models;

namespace SP_Share.Services
{
    public class GroupService : DefaultService
    {
        public GroupService()
        {
        }

        public Group[] GetGroupList(bool isAll)
        {
            if (isAll)
                return db.Group.ToArray();
            else
                return db.Group.Where(x => x.IsActive).ToArray();
        }

        public Group GetGroup(int? idx)
        {
            return db.Group.FirstOrDefault(x => x.Idx == idx);
        }

        public bool Insert(string name, string isadmin)
        {
            bool result = false;

            try
            {
                Group group = new Group();

                group.Name = name;

                if (isadmin == "True") group.IsActive = true;

                db.Entry(group).State = EntityState.Added;
                db.SaveChanges();

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public bool Active(int? idx)
        {
            bool result = false;

            try
            {
                Group group = db.Group.FirstOrDefault(x => x.Idx == idx);

                if (group != null)
                {
                    group.IsActive = true;

                    db.Entry(group).State = EntityState.Modified;
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

        public bool Limit(int? idx, int? limit, string size)
        {
            bool result = false;

            try
            {
                Group group = db.Group.FirstOrDefault(x => x.Idx == idx);

                if (group != null && limit != null)
                {
                    group.Limit = (int)limit;
                    group.Size = size;

                    db.Entry(group).State = EntityState.Modified;
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