﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPP_SIBKMNET.Context;
using WebApplicationMVC_SIBKM.Handler;
using WebApplicationMVC_SIBKM.Models;
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
       
        public ForgotPass Forgot(Forgot forgot)
        {
            var defpass = Hashing.HashPassword(forgot.DefPass);
            var data = myContext.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.User.Employee)
                .FirstOrDefault(x => x.User.Employee.Email.Equals(forgot.Email));
            var verify = Hashing.ValidatePassword(forgot.Default, defpass);

            if (verify)
            {
                var fpass = new ForgotPass()
                {
                    Id = data.User.Employee.Id,
                    Role = data. Role.Name,
                    Email = data.User.Employee.Email,
                    
                };
                return fpass;
            }

            return null;
        }

        public int EditAcc(EditAcc editAcc)
        {
            var oldpass = editAcc.OldPass;
            var newpass = editAcc.NewPass;
            var data = myContext.UserRoles
                  .Include(x => x.Role)
                  .Include(x => x.User)
                  .Include(x => x.User.Employee)
                  .FirstOrDefault(x => x.User.Employee.Email.Equals(editAcc.Email));
            int id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(editAcc.Email)).Id;
            var verify = Hashing.ValidatePassword(editAcc.OldPass, data.User.Password);

            if (verify)
            {
                User user = new User()
                {
                    
                    Password = Hashing.HashPassword(newpass)
                };
                myContext.Users.Update(user);
                var resultUser = myContext.SaveChanges();
            }
            return 0;
        }

        public int Register(Register register)
        {
            try
            {
                Employee employee = new Employee()
                {
                    FullName = register.FullName,
                    Email = register.Email
                };
                myContext.Employees.Add(employee);

                var resultEmployee = myContext.SaveChanges();
                if (resultEmployee > 0)
                {
                    int id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email)).Id;
                    User user = new User()
                    {
                        Id = id,
                        Password = Hashing.HashPassword(register.Password)
                    };
                    myContext.Users.Add(user);
                    var resultUser = myContext.SaveChanges();
                    if (resultUser > 0)
                    {
                        UserRole userRole = new UserRole()
                        {
                            UserId = id,
                            RoleId = register.RoleId

                        };
                        myContext.UserRoles.Add(userRole);
                        var resultUserRole = myContext.SaveChanges();
                        if (resultUserRole > 0)
                        {
                            return resultUserRole;
                        }
                        myContext.Users.Remove(user);
                        myContext.SaveChanges();
                        myContext.Employees.Remove(employee);
                        myContext.SaveChanges();
                        return 0;
                    }
                    myContext.Employees.Remove(employee);
                    myContext.SaveChanges();
                    return 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
            return 0;

        }
        public ResponseLogin Login(Login login)
        {
            var data = myContext.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.User.Employee)
                .FirstOrDefault(x => x.User.Employee.Email.Equals(login.Email));
            var verify = Hashing.ValidatePassword(login.Password, data.User.Password);

            if (verify)
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