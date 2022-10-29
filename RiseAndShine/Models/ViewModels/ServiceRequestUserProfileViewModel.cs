using Microsoft.AspNetCore.Mvc;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;

namespace RiseAndShine.Models.ViewModels
{
    public class ServiceRequestUserProfileViewModel
    {
        public List<ServiceRequest> ServiceRequests { get; set; }
        public ServiceRequest ServiceRequest { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<PackageType> PackageTypes { get; set; }
        public PackageType PackageType { get; set; }
        [BindProperty]
        public Vehicle Vehicle { get; set; } 
      
    }
}
