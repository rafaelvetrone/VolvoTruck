using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TruckPortal.Models;
using TruckPortal.DAL;
using TruckPortal.Entities;

namespace TruckPortal.Controllers
{
    public class HomeController : Controller
    {
        TruckContext db = new TruckContext();        

        public IActionResult Index()
        {
            List<Truck> trucks = db.Trucks.ToList();

            List<TruckViewModel> trucksVm = trucks.Select(x => new TruckViewModel()
            {
                Id = x.Id,
                ModelId = x.ModelId,
                ModelName = x.ModelName,
                DeliveryYear = x.DeliveryYear,
                ModelYear = x.ModelYear
            }).ToList();

            return View(trucksVm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            //ViewData["Message"] = "Your application description page.";

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem //adiciona uma opção que convida a escolher uma das possíveis opções
            {
                Text = "Select a model",
                Value = ""
            });

            list.Add(new SelectListItem //adiciona uma opção que convida a escolher uma das possíveis opções
            {
                Text = Enum.GetName(typeof(Model), 1),
                Value = "1"
            });

            list.Add(new SelectListItem //adiciona uma opção que convida a escolher uma das possíveis opções
            {
                Text = Enum.GetName(typeof(Model), 2),
                Value = "2"
            });

            ViewBag.ModelList = list;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TruckViewModel collection)
        {
            if (collection.DeliveryYear != DateTime.Now.Year)
            {
                ModelState.AddModelError("DeliveryYear", "Delivery year must be current year");
                return View(collection);
            }

            if (collection.ModelYear < collection.DeliveryYear || collection.ModelYear > collection.DeliveryYear + 1)
            {
                ModelState.AddModelError("ModelYear", "Model year must be current year or next year");
                return View(collection);
            }

            Truck truck = new Truck();
            truck.ModelId = collection.ModelId;
            truck.ModelName = Enum.GetName(typeof(Model), truck.ModelId);
            truck.ModelYear = collection.ModelYear;
            truck.DeliveryYear = collection.DeliveryYear;

            db.Trucks.Add(truck);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Truck truck = db.Trucks.Find(id);

            TruckViewModel truckVm = new TruckViewModel();
            truckVm.Id = truck.Id;
            truckVm.ModelId = truck.ModelId;
            truckVm.ModelName = truck.ModelName;
            truckVm.DeliveryYear = truck.DeliveryYear;
            truckVm.ModelYear = truck.ModelYear;

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem //adiciona uma opção que convida a escolher uma das possíveis opções
            {
                Text = "Select a model",
                Value = ""
            });

            list.Add(new SelectListItem //adiciona uma opção que convida a escolher uma das possíveis opções
            {
                Text = Enum.GetName(typeof(Model), 1),
                Value = "1"
            });

            list.Add(new SelectListItem //adiciona uma opção que convida a escolher uma das possíveis opções
            {
                Text = Enum.GetName(typeof(Model), 2),
                Value = "2"
            });

            ViewBag.ModelList = list;

            return View(truckVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TruckViewModel collection)
        {
            if (collection.DeliveryYear != DateTime.Now.Year)
            {
                ModelState.AddModelError("DeliveryYear", "Delivery year must be current year");
                return View(collection);
            }

            if (collection.ModelYear < collection.DeliveryYear || collection.ModelYear > collection.DeliveryYear + 1)
            {
                ModelState.AddModelError("ModelYear", "Model year must be current year or next year");
                return View(collection);
            }

            Truck truck = db.Trucks.Find(id);
            truck.ModelId = collection.ModelId;
            truck.ModelName = Enum.GetName(typeof(Model), truck.ModelId);
            truck.ModelYear = collection.ModelYear;
            truck.DeliveryYear = collection.DeliveryYear;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Truck truck = db.Trucks.Find(id);

            TruckViewModel truckVm = new TruckViewModel();
            truckVm.Id = truck.Id;
            truckVm.ModelId = truck.ModelId;
            truckVm.ModelName = truck.ModelName;
            truckVm.DeliveryYear = truck.DeliveryYear;
            truckVm.ModelYear = truck.ModelYear;

            return View(truckVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, TruckViewModel collection)
        {
            Truck truck = db.Trucks.Find(id);

            db.Trucks.Remove(truck);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
