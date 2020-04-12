using LocalDestination.Models;
using LocalDestination.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalDestination.WebMVC.Controllers
{
    [Authorize]
    public class HotelController : Controller
    {
        // GET: Hotel
        public ActionResult Index()
        {
            {
                var service = CreateHotelService();
                var model = service.GetHotels();

                return View(model);
            }
        }

            // GET: Note/Create
            public ActionResult Create()
            {
                PopulateDestinations();

                return View();
            }

        // POST: Hotel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelCreate model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDestinations();
                return View(model);
            }

            var service = CreateHotelService();

            if (service.CreateHotel(model))
            {
                TempData["SaveResult"] = "Your Hotel was created.";
                return RedirectToAction("Index");
            }

            PopulateDestinations();
            ModelState.AddModelError("", "Hotel could not be created");
            return View(model);
        }

        // GET: Hotel/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateHotelService();
            var model = service.GetHotelById(id);
            return View(model);
        }

        // GET: Hotel/Edit/{id}
        public ActionResult Edit(int id)
        {

            var service = CreateHotelService();
            var detail = service.GetHotelById(id);
            var model =
                new HotelEdit
                {
                    HotelId = detail.HotelId,
                    Name = detail.Name,
                    Address = detail.Address,
                    Comment = detail.Comment
                };
            PopulateDestinations(detail.DestinationId);
            return View(model);
        }

        // POST: Hotel/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HotelEdit model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDestinations(model.DestinationId);

                return View(model);
            }

            if (model.HotelId != id)
            {
                PopulateDestinations(model.DestinationId);
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateHotelService();

            if (service.UpdateHotel(model))
            {
                TempData["SaveResult"] = "Your Hotel was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Hotel could not be updated.");
            return View();
        }

        // GET: Hotel/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateHotelService();
            var model = service.GetHotelById(id);

            return View(model);
        }

        // POST: Hotel/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateHotelService();
            service.DeleteHotel(id);

            TempData["SaveResult"] = "Your Hotel was deleted.";
            return RedirectToAction("Index");
        }

        private HotelService CreateHotelService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new HotelService(userId);
            return service;
        }


        private void PopulateDestinations()
        {
            ViewBag.DestinationId = new SelectList(new DestinationService(Guid.Parse(User.Identity.GetUserId())).GetDestinations(), "DestinationId", "Name");
        }
        private void PopulateDestinations(int id)
        {
            ViewBag.DestinationId = new SelectList(new DestinationService(Guid.Parse(User.Identity.GetUserId())).GetDestinations(), "DestinationId", "Name", id);
        }
    }
}