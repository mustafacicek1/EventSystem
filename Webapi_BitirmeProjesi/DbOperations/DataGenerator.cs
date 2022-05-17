using System;
using System.Linq;
using Webapi_BitirmeProjesi.Common;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize()
        {
            using (var context= new EventSystemDbContext())
            {
                var admin = context.Users.SingleOrDefault(u => u.Mail == "admin@admin.com");
                if (admin!=null)
                {
                    return;
                }
                context.Users.Add(new User()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Mail = "admin@admin.com",
                    Role = RoleEnum.Admin.ToString(),
                    Password = "12345"
                });
                context.SaveChanges();
            }
        }
    }
}
