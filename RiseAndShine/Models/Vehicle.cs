﻿using RiseAndShine.Auth.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseAndShine.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        [DataType(DataType.Date)]
        public DateTime ManufactureDate { get; set; }
        public int OwnerId { get; set; }
        public string ImageUrl { get; set; }    

    }
}
