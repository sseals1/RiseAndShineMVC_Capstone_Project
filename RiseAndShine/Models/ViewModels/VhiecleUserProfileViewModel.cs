using System.Collections.Generic;

namespace RiseAndShine.Models.ViewModels
{
    public class VhiecleUserProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Vehicle> Vehicles  { get; set; }
        public PackageType PackageType { get; set; }
        public Vehicle Vehicle { get; set; }    

    }
}
