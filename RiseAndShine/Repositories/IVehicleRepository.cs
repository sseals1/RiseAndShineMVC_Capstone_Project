using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace RiseAndShine.Models
{
    public interface IVehicleRepository
    {
        SqlConnection Connection { get; }

        void Add(Vehicle vehicle);
        List<Vehicle> GetVehicleByOwnerIdWithServiceRequests(int ownerId);
        Vehicle GetVehicleByCarId(int CarId);
        List<Vehicle> GetVehiclesByOwnerId(int ownerId);
        void DeleteVehicle(int id);
    }
}