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
    public class BarController : Controller
    {
        // GET: Bar
        public ActionResult Index()
        {
            {
                var service = CreateBarService();
                var model = service.GetBars();

                return View(model);
            }
        }

        // GET: Bar/Create
        public ActionResult Create()
        {
            PopulateDestinations();

            return View();
        }

        // POST: Bar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BarCreate model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDestinations();
                return View(model);
            }

            var service = CreateBarService();

            if (service.CreateBar(model))
            {
                TempData["SaveResult"] = "Your Bar was created.";
                return RedirectToAction("Index");
            }

            PopulateDestinations();
            ModelState.AddModelError("", "Bar could not be created");
            return View(model);
        }

        // GET: Bar/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateBarService();
            var model = service.GetBarById(id);
            return View(model);
        }

        // GET: Bar/Edit/{id}
        public ActionResult Edit(int id)
        {

            var service = CreateBarService();
            var detail = service.GetBarById(id);
            var model =
                new BarEdit
                {
                    BarId = detail.BarId,
                    Name = detail.Name,
                    Address = detail.Address,
                    Comment = detail.Comment
                };
            PopulateDestinations(detail.DestinationId);
            return View(model);
        }

        // POST: Bar/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BarEdit model)
        {
            if (!ModelState.IsValid)
            {
                PopulateDestinations(model.DestinationId);

                return View(model);
            }

            if (model.BarId != id)
            {
                PopulateDestinations(model.DestinationId);
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateBarService();

            if (service.UpdateBar(model))
            {
                TempData["SaveResult"] = "Your Bar was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Bar could not be updated.");
            return View();
        }

        // GET: Bar/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateBarService();
            var model = service.GetBarById(id);

            return View(model);
        }

        // POST: Bar/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateBarService();
            service.DeleteBar(id);

            TempData["SaveResult"] = "Your Bar was deleted.";
            return RedirectToAction("Index");
        }

        private BarService CreateBarService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BarService(userId);
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