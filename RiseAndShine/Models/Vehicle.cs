using RiseAndShine.Auth.Models;
using System;

namespace RiseAndShine.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public DateTime ManufactureDate { get; set; }

    }
}
