using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPP_SIBKMNET.Context;
using WebApplicationMVC_SIBKM.ViewModels;

namespace WebApplicationMVC_SIBKM.Repositories.Data
{
    public class AccountRepository
    {
        MyContext myContext;

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public ResponseLogin Login(Login login)
        {
            var data = myContext.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.User.Employee)
                .FirstOrDefault(x => x.User.Employee.Email.Equals(login.Email) &&
                                     x.User.Password.Equals(login.Password));

            if (data != null)
            {
                var response = new ResponseLogin()
                {
                    Id = data.User.Employee.Id,
                    FullName = data.User.Employee.FullName,
                    Email = data.User.Employee.Email,
                    Role = data.Role.Name
                };
                return response;
            }
            return null;
        }

    }
}