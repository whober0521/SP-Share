using System.Data.Entity.Infrastructure;
using SP_Share.Models;

namespace SP_Share.Services
{
    public class DefaultService
    {
        public SPContext db;

        public DefaultService()
        {
            db = new SPContext();

            ((IObjectContextAdapter)db).ObjectContext.CommandTimeout = 180;
        }
    }
}