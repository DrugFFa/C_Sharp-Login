using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPP_SIBKMNET.Context;
using WebApplicationMVC_SIBKM.Models;

namespace WebApplicationMVC_SIBKM.Controllers
{
    public class KelurahanController : Controller
    {
        MyContext myContext;

        public KelurahanController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        //GETALL
        public IActionResult index()
        {
            var data = myContext.Kelurahan.Include(x => x.Kecamatan).ToList();
            return View(data);
        }

        //GetByID

        public IActionResult Details(int id)
        {
            var data = myContext.Kelurahan.Include(x => x.Kecamatan).FirstOrDefault(x => x.Id.Equals(id));
            return View(data);
        }

        //Create
        //GET

        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Kelurahan kelurahan)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    myContext.Kelurahan.Add(kelurahan);
                    var result = myContext.SaveChanges();
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }


            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = myContext.Kelurahan.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Kelurahan Model)
        {
            var data = myContext.Kelurahan.Where(x => x.Id == Model.Id).FirstOrDefault();
            if (data != null)
            {
                data.Id = Model.Id;
                data.Name = Model.Name;
                data.KecamatanId = Model.KecamatanId;

                myContext.SaveChanges();
            }

            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            var data = myContext.Kelurahan.Where(x => x.Id == id).FirstOrDefault();
            myContext.Kelurahan.Remove(data);
            myContext.SaveChanges();
            ViewBag.Messsage = "Record Delete Successfully";
            return RedirectToAction("index");
        }
    }
}

