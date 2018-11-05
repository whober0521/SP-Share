using SP_Share.Models;
using System.Data.Entity;
using System.Linq;
using System;

namespace SP_Share.Services
{
    public class UserService : DefaultService
    {
        public UserService()
        {

        }

        public User Login(string account, string password)
        {
            return db.User.FirstOrDefault(x => x.Account == account && x.Password == password && x.IsActive);
        }

        public Group[] GetGroupList()
        {
            return db.Group.Where(x => x.IsActive).ToArray();
        }

        public bool Insert(User user)
        {
            bool result = false;

            try
            {
                db.Entry(user).State = EntityState.Added;
                db.SaveChanges();

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