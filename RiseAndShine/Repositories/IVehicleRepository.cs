using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace RiseAndShine.Models
{
    public interface IVehicleRepository
    {
        SqlConnection Connection { get; }

        void Add(Vehicle vehicle);
        List<Vehicle> GetVehicleByOwnerId(int ownerId);

    }
}