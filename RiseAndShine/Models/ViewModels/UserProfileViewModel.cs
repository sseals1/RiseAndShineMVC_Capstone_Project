using RiseAndShine.Models;
using System;
using System.Collections.Generic;


namespace RiseAndShine.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Vehicle> Vehicle { get; set; }    
        //public List<ServiceProvider> ServiceProvider { get; set; }   
    }
}

