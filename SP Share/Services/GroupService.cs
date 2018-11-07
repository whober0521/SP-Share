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

        public bool Insert(string name)
        {
            bool result = false;

            try
            {
                Group group = new Group();

                group.Name = name;

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

        public bool Update(int? idx)
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
    }
}