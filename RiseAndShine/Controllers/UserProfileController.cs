using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseAndShine.Models;
using RiseAndShine.Repositories;
using RiseAndShine.Models.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Amazon.EC2.Model;
using System.Xml.Linq;
using System.Linq;
using System;

namespace RiseAndShine.Controllers
{
    public class UserProfileController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            List<UserProfile> userProiles = _userProfileRepo.GetAllUserProfiles();

            return View(userProiles);
        }

        //GET: UserProfileController/Details/5
        [HttpGet]
        //public ActionResult Details(string firebaseUserId)
        //{
        //    UserProfile userProfile = _userProfileRepo.GetByFirebaseUserId(firebaseUserId);
        //    //var userTypes = _userTypeRepository.GetAllUserTypes();
        //    //userProfile.UserTypes = userTypes;
        //    return View(userProfile);
        //}
        [Authorize]
        [HttpGet]
        public ActionResult Details()
        {
            var ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var availableSRList = _serviceRequestRepo.GetAllAvailableServiceRequests();
            UserProfile userProfile = _userProfileRepo.GetUserProfileById(ownerId);
            List<Vehicle> vehicles = _vehicleRepository.GetVehicleByOwnerIdWithServiceRequests(ownerId);
         
         
            var vIds = vehicles.Select(v => v.Id).ToList();


            List<ServiceRequest> serviceRequests = new List<ServiceRequest>();
            foreach (var Id in vIds)
            {
                var srByVehicle = _serviceRequestRepo.GetServiceRequestByVehicleId(Id);               
                serviceRequests.AddRange(srByVehicle);
            }

            var serviceRequestsWithCarId = _serviceRequestRepo.GetAllServiceRequestsWithCarId()
                .FindAll(c => vehicles.Find(v => v.Id == c.Id)!=null);
            foreach (ServiceRequest sr in serviceRequestsWithCarId)
            {
                sr.Vehicle = _vehicleRepository.GetVehicleByCarId(sr.CarId);               
            }
         
            UserProfileViewModel vm = new UserProfileViewModel()
            {
                UserProfile = userProfile,
                Vehicles = vehicles,
                ServiceRequests = serviceRequestsWithCarId,
                AvailableServiceRequests = availableSRList
                
            };

            return View(vm);



        }

        // GET: UserProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController/Create
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

        // GET: UserProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            ServiceRequest serviceRequest = _serviceRequestRepo.GetServiceRequestById(id);
            var ServiceRequest = new ServiceRequest();
             List<PackageType> packageTypes = _packageTypeRepo.GetAll();
             serviceRequest.Package = _packageTypeRepo.GetPackageTypeById(id);

            UserProfileViewModel vm = new UserProfileViewModel()
            {
                ServiceRequest = serviceRequest,
                //PackageType = packageType,
                PackageTypes = packageTypes
             
            };
            return View(vm);
        }

        // POST: UserProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ServiceRequest serviceRequest)
        {
            try
            {
                _serviceRequestRepo.UpdateServiceRequest(serviceRequest);


                return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                return View(serviceRequest);
            }
        }

        // GET: UserProfileController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ServiceRequest serviceRequest = _serviceRequestRepo.GetServiceRequestById(id);
            serviceRequest.Package = _packageTypeRepo.GetPackageTypeById(id);
            UserProfileViewModel vm = new UserProfileViewModel()
            {

                ServiceRequest = serviceRequest,
            };
            return View(vm);
        }

        // POST: UserProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ServiceRequest serviceRequest)
        {
            try
            {
                _serviceRequestRepo.DeleteServiceRequest(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(serviceRequest);
            }
        }

        private readonly IUserProfileRepository _userProfileRepo;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPackageTypeRepository _packageTypeRepo;
        private readonly IServiceRequestRepository _serviceRequestRepo;
       

        // ASP.NET will give us an instance of our UserProfile  Repository. This is called "Dependency Injection"
        public UserProfileController(IUserProfileRepository userProfileRepository, IVehicleRepository vehicleRepository, IServiceRequestRepository serviceRequestRepository, IPackageTypeRepository packageTypeRepository)
        {
            _serviceRequestRepo = serviceRequestRepository;
            _userProfileRepo = userProfileRepository;
            _vehicleRepository = vehicleRepository;
            _packageTypeRepo = packageTypeRepository;
        }
    }
}
