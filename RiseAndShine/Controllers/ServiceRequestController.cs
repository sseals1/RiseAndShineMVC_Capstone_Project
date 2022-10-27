using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseAndShine.Models;

namespace RiseAndShine.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequestRepository _serviceRequestRepo;
        public ServiceRequestController(IServiceRequestRepository serviceRequestRepository)
        {
            _serviceRequestRepo = serviceRequestRepository;
            
        }
        // GET: ServiceRequestController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServiceRequestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServiceRequestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ServiceRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            ServiceRequest serviceRequest = _serviceRequestRepo.GetServiceRequestById(id);
            return View(serviceRequest);
        }

        // POST: ServiceRequestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ServiceRequest serviceRequest)
        {
            try
            {
                //_serviceRequestRepo.Delete(id, serviceRequest);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
