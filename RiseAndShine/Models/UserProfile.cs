namespace RiseAndShine_HomeCarWash.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name
        {
            set
            {
                Name = FirstName + LastName;
            }
        }
        public string ImageURL { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int UserTypeId { get; set; }
        public int NeighborhoodId { get; set; }
    }
}
