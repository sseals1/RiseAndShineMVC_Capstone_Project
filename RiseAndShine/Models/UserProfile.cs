using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiseAndShine.Models
{
    public class UserProfile
    {
        public string FirebaseUserId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string ImageURL { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Display(Name = "User Type")]
        public UserType UserType { get; set; }
        public List<UserType> UserTypes { get; set; }
        public int UserTypeId { get; set; }
        public int NeighborhoodId { get; set; }
        public PackageType PackageType { get; set; }
        public ServiceRequest ServiceRequest { get; set; }
        
    }
}
