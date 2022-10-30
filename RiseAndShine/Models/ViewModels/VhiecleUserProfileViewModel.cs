using System.Collections.Generic;

namespace RiseAndShine.Models.ViewModels
{
    public class VehicleUserProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public PackageType PackageType { get; set; }
        public Vehicle Vehicle { get; set; }
        public ServiceRequest ServiceRequest { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; }

    }
}
