using Microsoft.SharePoint.Client;
using System.Collections.Generic;

namespace RiseAndShine.Models
{
    public class PackageType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<PackageType> PackageTypes { get; set; } 
    }
}
