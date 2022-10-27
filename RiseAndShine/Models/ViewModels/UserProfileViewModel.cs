using Microsoft.Extensions.DependencyInjection;
using RiseAndShine.Models;
using System;
using System.Collections.Generic;


namespace RiseAndShine.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Vehicle> Vehicles { get; set; } 
        public Vehicle Vehicle { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; } 
        public List<PackageType> PackageTypes { get; set; }
        public PackageType PackageType { get; set; }   
        public ServiceRequest ServiceRequest { get; set; }
      
    }
}

