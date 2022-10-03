using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebAPP_SIBKMNET.Context;
using WebAPP_SIBKMNET.Models;
using WebApplicationMVC_SIBKM.Repositories.Data;

namespace WebApplicationMVC_SIBKM.Controllers
{
    public class ProvinceController : Controller
    {
        ProvinceRepository ProvinceRepository;

        public ProvinceController(ProvinceRepository ProvinceRepository)
        {
            this.ProvinceRepository = ProvinceRepository;
        }

        Province province = new Province();
        //GET ALL
        //GET
        public IActionResult Index()
        {
            //var data = myContext.Provinces.Include(x => x.Region).ToList(); //untuk get data dari database menggunakan entity framework core
            string role = HttpContext.Session.GetString("Role");
            
            if (role.Equals("Admin"))
            {
                var data = ProvinceRepository.Get();
                return View(data);
            }
            return RedirectToAction("Unauthorized", "ErrorPage");
        }

        //GET BY ID
        public IActionResult Details(int id)
        {
            //var data = myContext.Provinces.Include(x => x.Region).FirstOrDefault(x => x.Id.Equals(id));
            var data = ProvinceRepository.Get(id);
            return View(data);
        }
        //CREATE
        //GET
        public IActionResult Create()
        {
            return View(province);
        }

        //POST - ngirim data berupa data yang sesuai dengan di model
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Province province)
        {
            if (ModelState.IsValid)
            {
                //myContext.Provinces.Add(province);
                //var result = myContext.SaveChanges();
                var result = ProvinceRepository.Post(province);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(province);
        }

        //UPDATE
        //GET
        public IActionResult Edit(int id)
        {
            //var data = myContext.Provinces.Find(id);
            //return View(data);
            var data = ProvinceRepository.Get(id);
            return View(data);
        }

        //POST - ngirim data berupa data yang sesuai dengan di model
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Province province)
        {
            if (ModelState.IsValid)
            {
                //var data = myContext.Provinces.Find(id);
                //data.Name = province.Name;
                //data.RegionId = province.RegionId;
                //myContext.Provinces.Update(data);
                //var result = myContext.SaveChanges();
                var result = ProvinceRepository.Put(id, province);
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View();
        }

        //DELETE
        //GET
        public IActionResult Delete(int id)
        {
            //var data = myContext.Provinces.Find(id);
            //return View(data);
            var data = ProvinceRepository.Get(id);
            return View(data);
        }

        //POST - ngirim data berupa data yang sesuai dengan di model
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(Province province)
        {
            //myContext.Provinces.Remove(province);
            //var result = myContext.SaveChanges();
            var result = ProvinceRepository.Delete(province);
            if (result > 0)
                return RedirectToAction("Index");
            return View();
        }
    }
}