using System.Data.Entity;
using System.Linq;
using System;
using SP_Share.Helpers;
using SP_Share.Models;

namespace SP_Share.Services
{
    public class UserService : DefaultService
    {
        public UserService()
        {

        }

        public User[] GetUserList()
        {
            return db.User.ToArray();
        }

        public User GetUser(string account)
        {
            return db.User.FirstOrDefault(x => x.Account == account);
        }

        public User Login(string account, string password)
        {
            User user = db.User.FirstOrDefault(x => x.Account == account && x.IsActive);

            string pwd = "";

            if (user != null)
            {
                string salt = user.Salt;

                pwd = SecurityHelper.HashPassword(password, ref salt);

                if (pwd != user.Password) user = null;
            }

            return user;
        }

        public bool Insert(User user)
        {
            bool result = false;

            try
            {
                string salt = "";

                user.Password = SecurityHelper.HashPassword(user.Password, ref salt);
                user.Salt = salt;

                db.Entry(user).State = EntityState.Added;

                if (user.Group != null)
                {
                    UserGroup ug = new UserGroup();

                    ug.User = user.Account;
                    ug.Group = (int)user.Group;

                    db.Entry(ug).State = EntityState.Added;
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

        public bool Active(string account)
        {
            bool result = false;

            try
            {
                User user = db.User.FirstOrDefault(x => x.Account == account);

                if (user != null)
                {
                    user.IsActive = true;

                    db.Entry(user).State = EntityState.Modified;
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

        public bool Limit(string account, int? limit, string size)
        {
            bool result = false;

            try
            {
                User user = db.User.FirstOrDefault(x => x.Account == account);

                if (user != null && limit != null)
                {
                    user.Limit = (int)limit;
                    user.Size = size;

                    db.Entry(user).State = EntityState.Modified;
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