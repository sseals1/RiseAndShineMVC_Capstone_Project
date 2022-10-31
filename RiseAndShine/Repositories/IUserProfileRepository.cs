
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace RiseAndShine.Models
{
    public interface IUserProfileRepository
    {
        SqlConnection Connection { get; }

        void Add(UserProfile userProfile);
        List<UserProfile> GetAllUserProfiles();
        UserProfile GetByFirebaseUserId(string FirebaseUserId);
        UserProfile GetUserProfileById(int id);
        List<UserProfile> GetAllOwners();
        List<UserProfile> GetUserProfileByServiceProviderId(int id);
    }
}