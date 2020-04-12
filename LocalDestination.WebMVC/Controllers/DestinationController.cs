using LocalDestination.Models;
using LocalDestination.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LocalDestination.WebMVC.Controllers
{
    public class DestinationController : Controller
    {
        // GET: Destination
        [HttpGet]
        public ActionResult Index()
        {
            var service = CreateDestinationService();
            var model = service.GetDestinations();
            return View(model);
        }

        // GET: Destination/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Destination/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DestinationCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateDestinationService();

            if (service.CreateDestination(model))
            {
                TempData["SaveResult"] = "Your Destination was created";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Destination could not be created");
            return View(model);
        }

        // GET: Destination/Details/{id}
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateDestinationService();
            var model = service.GetDestinationById(id.Value);
            return View(model);
        }

        // GET: Destination/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateDestinationService();
            try
            {
                var detail = service.GetDestinationById(id.Value);

                var model = new DestinationEdit
                {
                    DestinationId = detail.DestinationId,
                    Name = detail.Name,
                    City = detail.City,
                    State = detail.State,
                    Country = detail.Country
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Destination/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DestinationEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (id != model.DestinationId)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateDestinationService();

            if (service.UpdateDestination(model))
            {
                TempData["SaveResult"] = "Destination updated";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Destination could not be updated");
            return View(nameof(Index));
        }

        //GET: Destination/Delete/{id}
        [HttpGet]
        [ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                var service = CreateDestinationService();
                var model = service.GetDestinationById(id.Value);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //POST: Destination/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateDestinationService();

            if (service.DeleteDestination(id))
            {
                TempData["SaveResult"] = "Destination was deleted.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Delete), id);
        }

        private DestinationService CreateDestinationService() => new DestinationService(Guid.Parse(User.Identity.GetUserId()));
    }
}