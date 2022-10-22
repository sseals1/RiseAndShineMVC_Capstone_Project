using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseAndShine.Auth.Models;
using RiseAndShine.Models;

namespace RiseAndShine.Controllers
{
    public class VehicleController : Controller
    {
        // GET: CarController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: CarController1/Details/5
        public ActionResult Details(int id)
        {
            Vehicle vehicle = _vehicleRepo.GetVehicleById(id);           
            return View(vehicle);
        }

        // GET: CarController1/Create
        public ActionResult CreateVehicle(Vehicle vehicle)
        {
            //var newVehicle = new Vehicle();
            //{
            //    Make = newVehicle.Make,
            //    Model = newVehicle.Model;
            //    Color = newVehicle.Color
            //    ManufactureDate = newVehicle.ManufactureDate
               
            //};

            //_vehicleRepository.Add(newVehicle);
            return RedirectToAction("UPWithCars", "UserProfile");
        }

        // POST: CarController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                //var newVehicle = new Vehicle();
                //{
                //    Make = newVehicle.Make,
                //    Model = newVehicle.Model;
                //    Color = newVehicle.Color
                //    ManufactureDate = newVehicle.ManufactureDate

                //};

                //return RedirectToAction("UPWithCars", "UserProfile");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //_vehicleRepository.Add(newVehicle);
                return View();
            }
        }

        // GET: CarController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarController1/Edit/5
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

        // GET: CarController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
        private readonly IVehicleRepository _vehicleRepo;
        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepo = vehicleRepository;
        }
    }
}
