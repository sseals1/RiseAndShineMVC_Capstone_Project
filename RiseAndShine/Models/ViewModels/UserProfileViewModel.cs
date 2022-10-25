﻿using Microsoft.Extensions.DependencyInjection;
using RiseAndShine.Models;
using System;
using System.Collections.Generic;


namespace RiseAndShine.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<Vehicle> Vehicles { get; set; }    
        public List<ServiceRequest> ServiceRequests { get; set; } 
      
    }
}

