using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseAndShine.Models;
using RiseAndShine.Repositories;
using RiseAndShine.Models.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

            UserProfile userProfile = _userProfileRepo.GetUserProfileById(ownerId);
            List<Vehicle> vehicle = _vehicleRepository.GetVehicleByOwnerId(ownerId);
            List<ServiceRequest> serviceRequests = _serviceRequestRepo.GetServiceRequestByUserId(ownerId);
            //var userTypes = _userTypeRepository.GetAllUserTypes();
            //userProfile.UserTypes = userTypes;

            UserProfileViewModel vm = new UserProfileViewModel()
            {
                UserProfile = userProfile,
                Vehicle = vehicle,  
                ServiceRequest = serviceRequests
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
            return View();
        }

        // POST: UserProfileController/Edit/5
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

        // GET: UserProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserProfileController/Delete/5
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

        private readonly IUserProfileRepository _userProfileRepo;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IServiceRequestRepository _serviceRequestRepo;

        // ASP.NET will give us an instance of our UserProfile  Repository. This is called "Dependency Injection"
        public UserProfileController(IUserProfileRepository userProfileRepository, IVehicleRepository vehicleRepository, IServiceRequestRepository serviceRequestRepository)
        {
            _serviceRequestRepo = serviceRequestRepository;
            _userProfileRepo = userProfileRepository;
            _vehicleRepository = vehicleRepository; 
        }
    }
}
