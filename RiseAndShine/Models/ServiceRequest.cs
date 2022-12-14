using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseAndShine.Models
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; }
        public List<ServiceRequest> AvailableServiceRequests { get; set; }
        public int CarId { get; set; }
        public int DetailTypeId { get; set; }      
        public DateTime Date { get; set; }
        public DateTime ServiceDate { get; set; }
        public int ServiceProviderId { get; set; }
        public string Note { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<ServiceRequest> UserProfiles { get; set; }
        public PackageType PackageType { get; set; }
        public Vehicle Vehicle { get; set; }


    }
}
