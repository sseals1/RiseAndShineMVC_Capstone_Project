﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SharePoint.Client;
using RiseAndShine.Models;
using RiseAndShine.Models.ViewModels;
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
        public ServiceRequestController(IServiceRequestRepository serviceRequestRepository, IUserProfileRepository userProfileRepository)
        {
            _serviceRequestRepo = serviceRequestRepository;
            _userProfileRepo = userProfileRepository;

        }
        // GET: ServiceRequestController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ServiceRequestController/Details/5
        public ActionResult Details(int id)
        {
            UserProfile userProfile = new UserProfile();
            List<ServiceRequest> availableServiceRequests = new List<ServiceRequest>();
            availableServiceRequests = _serviceRequestRepo.GetAllAvailableServiceRequests();
            userProfile = _userProfileRepo.GetUserProfileById(id);

            foreach (ServiceRequest serviceRequest in availableServiceRequests)
            {
                serviceRequest.UserProfile = _userProfileRepo.GetUserProfileById(serviceRequest.ServiceProviderId);
            }

            ServiceRequestUserProfileViewModel vm = new ServiceRequestUserProfileViewModel()
            {
                ServiceRequests = availableServiceRequests
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
        public ActionResult Edit(int carId)
        {
            return View();
        }

        // POST: ServiceRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceRequest serviceRequest)
        {
            try
            {

                var newServiceRequest = new ServiceRequest()
                {
                    CarId = serviceRequest.CarId,
                    DetailTypeId = serviceRequest.DetailTypeId,
                    ServiceDate = serviceRequest.ServiceDate,
                    ServiceProviderId = serviceRequest.ServiceProviderId,
                    Note = serviceRequest.Note,
                };
                _serviceRequestRepo.Add(newServiceRequest);
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
