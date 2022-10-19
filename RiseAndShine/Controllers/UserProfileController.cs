﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseAndShine_HomeCarWash.Models;
using System.Collections.Generic;

namespace RiseAndShine_HomeCarWash.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfileController
        // GET: Walkers
        public ActionResult Index()
        {
            List<UserProfile> userProiles = _userProfileRepo.GetAllUserProfiles();

            return View(userProiles);
        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepo = userProfileRepository;
        }
    }
}
