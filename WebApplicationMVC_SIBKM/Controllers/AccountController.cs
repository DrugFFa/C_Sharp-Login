using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationMVC_SIBKM.Repositories.Data;
using WebApplicationMVC_SIBKM.ViewModels;

namespace WebApplicationMVC_SIBKM.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Forgot()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult EditAcc()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditAcc(EditAcc editAcc)
        {
            //MASIH BELOM

            var data = accountRepository.EditAcc(editAcc);
            if (data > 0)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            var data = accountRepository.Register(register);
            if (data > 0)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                //statement mengambil data dari database sesuai dengan email dan password
                //return Id employee, FullName, Email, Role -> Masukkan ke ViewModels
                var data = accountRepository.Login(login);

                if (data != null)
                {
                    //inisialisasi nilai pada session
                    HttpContext.Session.SetString("Role", data.Role);
                    HttpContext.Session.SetInt32("Id", data.Id);
                    return RedirectToAction("Index", "Province");
                }
                return RedirectToAction("Unauthorized", "ErrorPage");
            }
            return View();
        }
        [HttpPost]

        public IActionResult Forgot(Forgot forgot)
        {
            if (ModelState.IsValid)
            {
                //statement mengambil data dari database sesuai dengan email dan password
                //return Id employee, FullName, Email, Role -> Masukkan ke ViewModels
                var data = accountRepository.Forgot(forgot);

                if (data != null)
                {
                    //inisialisasi nilai pada session
                    HttpContext.Session.SetString("Role", data.Role);
                    return RedirectToAction("Index", "Province");
                }
                return RedirectToAction("Unauthorized", "ErrorPage");
            }
            return View();
        }
    }
}