using SP_Share.Models;
using System.Linq;

namespace SP_Share.Services
{
    public class UserService : DefaultService
    {
        public UserService()
        {

        }

        public User Login(string account, string password)
        {
            return db.User.FirstOrDefault(x => x.Account == account && x.Password == password);
        }
    }
}