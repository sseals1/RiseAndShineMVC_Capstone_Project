using Microsoft.Data.SqlClient;
using RiseAndShine.Models;
using System.Collections.Generic;

namespace RiseAndShine.Repositories
{
    public interface IUserTypeRepository
    {
        SqlConnection Connection { get; }

        List<UserType> GetAllUserTypes();
    }
}