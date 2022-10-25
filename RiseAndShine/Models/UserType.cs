using System.Collections.Generic;

namespace RiseAndShine.Models
{
    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public UserType UserTypes { get; set; }
    }
}
