using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;

namespace RiseAndShine.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; }
        public int CarId { get; set; }
        public int DetailTypeId { get; set; }
        public DateTime ServiceDate { get; set; }
        public int ServiceProviderId { get; set; }
        public string Note { get; set; }
        public UserProfile UserProfile { get; set; }
        public PackageType Package { get; set; }
        public Vehicle Vehicle { get; set; }


    }
}
