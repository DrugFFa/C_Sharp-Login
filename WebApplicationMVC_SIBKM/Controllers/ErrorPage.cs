using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationMVC_SIBKM.Controllers
{
    public class ErrorPage : Controller
    {
        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}
