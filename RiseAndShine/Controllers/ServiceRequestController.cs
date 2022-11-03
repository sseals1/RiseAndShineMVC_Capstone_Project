using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SharePoint.Client;
using RiseAndShine.Models;
using RiseAndShine.Models.ViewModels;
using RiseAndShine.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace RiseAndShine.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly IServiceRequestRepository _serviceRequestRepo;
        private readonly IUserProfileRepository _userProfileRepo;
        private readonly IPackageTypeRepository _packageTypeRepo;
        private readonly IVehicleRepository _vehicleRepo;
        public ServiceRequestController(IVehicleRepository vehicleRepository,IServiceRequestRepository serviceRequestRepository, IUserProfileRepository userProfileRepository, IPackageTypeRepository packageTypeRepository)
        {
            _serviceRequestRepo = serviceRequestRepository;
            _userProfileRepo = userProfileRepository;
            _packageTypeRepo = packageTypeRepository;
            _vehicleRepo = vehicleRepository;

        }
        // GET: ServiceRequestController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServiceRequestController/Details/5
        public ActionResult Details(int id)
        {
            
            
            var theVehicle = _vehicleRepo.GetVehicleByCarId(id);
            
            List<ServiceRequest> availableServiceRequests = new List<ServiceRequest>();
            // getting all service requests where CarId =NULL 
            availableServiceRequests = _serviceRequestRepo.GetAllAvailableServiceRequests();
            // itteration over the availableServiceRequests List and for each object in the list,
            // passing the object.ServiceProviderId in as an argument to the get mehthod that returns a single
            // UserProfile. 
            foreach (ServiceRequest serviceRequest in availableServiceRequests)
            {
                // Assigning the ServiceProviderI value to the UserProfile object property on the serviceRequest.
                serviceRequest.UserProfile = _userProfileRepo.GetUserProfileById(serviceRequest.ServiceProviderId);
                serviceRequest.CarId = id;
                serviceRequest.ServiceProviderId = serviceRequest.ServiceProviderId;
            }

            List<PackageType> packageTypes = new List<PackageType>();
            packageTypes = _packageTypeRepo.GetAll();

            ServiceRequestUserProfileViewModel vm = new ServiceRequestUserProfileViewModel()
            {
                ServiceRequests = availableServiceRequests,
                PackageTypes = packageTypes,
                Vehicle = theVehicle
            };

            return View(vm);

        }


        // GET: ServiceRequestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceRequest serviceRequest)
        {
            
                return View();         
        }

        // GET: ServiceRequestController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
         
            return View();
        }

        // POST: ServiceRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,ServiceRequest serviceRequest)
        {
            try
            {

                
                var newServiceRequest = new ServiceRequest()
                {
                    ServiceProviderId = serviceRequest.ServiceProviderId,
                    Id = serviceRequest.Id,
                    CarId = serviceRequest.CarId,
                    DetailTypeId = serviceRequest.DetailTypeId,
                    Note = serviceRequest.Note,
                };
                _serviceRequestRepo.Update(newServiceRequest);
                return RedirectToAction("Details", "UserProfile");
            }
            catch (Exception ex)
            {
                return View(serviceRequest);
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
