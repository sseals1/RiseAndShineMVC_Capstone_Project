using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace RiseAndShine.Models
{
    public interface IServiceRequestRepository
    {
        SqlConnection Connection { get; }

        ServiceRequest GetServiceRequestById(int id);
        List<ServiceRequest> GetServiceRequestByVehicleId(int vehicleId);
    }
}