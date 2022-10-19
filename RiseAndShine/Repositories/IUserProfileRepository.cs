using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace RiseAndShine_HomeCarWash.Models
{
    public interface IUserProfileRepository
    {
       
        SqlConnection Connection { get; }

        List<UserProfile> GetAllUserProfiles();
        UserProfile GetUserProfileById(int id);
    }
}