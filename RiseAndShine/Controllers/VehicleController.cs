using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseAndShine.Auth.Models;
using RiseAndShine.Models;
using RiseAndShine.Repositories;
using System;
using System.Collections.Generic;
using RiseAndShine.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace RiseAndShine.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        // GET: CarController1
        [HttpGet]
        public ActionResult Index()
        {
            var ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //List<Vehicle> vehicles = _vehicleRepo.GetVehicleByOwnerIdWithServiceRequests(ownerId);
            //var vIds = vehicles.Select(v => v.Id).ToList();
            // Itterating the vIds obj and passing each Id to the Method

            List<Vehicle> vehiclesByOwnerId = _vehicleRepo.GetVehiclesByOwnerId(ownerId);
            // Adding the gotten service requests assocciated with a vehicle, to the serviceRequests lsit
            //vehicles.AddRange(vehiclesByOwnerId);

            return View(vehiclesByOwnerId);
        }

        // GET: CarController1/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {

            List<Vehicle> vehicle = _vehicleRepo.GetVehicleByOwnerIdWithServiceRequests(id);
            return View(vehicle);
        }

        // GET: CarController1/Create/1
        public ActionResult Create()
        {
            return View();
        }

        //POST: CarController1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehicle vehicle)
        {

            try
            {
                var ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var newVehicle = new Vehicle
                {
                    Make = vehicle.Make,
                    Model = vehicle.Model,
                    Color = vehicle.Color,
                    ManufactureDate = vehicle.ManufactureDate,
                    OwnerId = ownerId

                };
                _vehicleRepo.Add(newVehicle);
                return RedirectToAction("Details", "UserProfile");
            }
            catch (Exception ex)
            {
                return View(vehicle);
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


            Vehicle vehicle = _vehicleRepo.GetVehicleByCarId(id);
            List<ServiceRequest> serviceRequest = _serviceRequestRepo.GetServiceRequestsByVehicleId(id);
            //serviceRequest.PackageType = _packageTypeRepo.GetPackageTypeById(id);
            VehicleUserProfileViewModel vm = new VehicleUserProfileViewModel()
            {

                Vehicle = vehicle,
                ServiceRequests = serviceRequest,
            };
            return View(vm);

        }

        // POST: CarController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Vehicle vehicle)
        {
            try
            {
                _vehicleRepo.DeleteVehicle(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(vehicle);
            }
        }
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IUserProfileRepository _userProfileRepo;
        private readonly IServiceRequestRepository _serviceRequestRepo;


        public VehicleController(IServiceRequestRepository serviceRequestRepository, IVehicleRepository vehicleRepository, IUserProfileRepository userProfileRepo)
        {
            _userProfileRepo = userProfileRepo;
            _vehicleRepo = vehicleRepository;
            _serviceRequestRepo = serviceRequestRepository;
        }
    }
}

