using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace RiseAndShine.Models
{
    public interface IServiceRequestRepository
    {
        SqlConnection Connection { get; }

        //List<ServiceRequest> GetServiceRequestByUserId(int id);
        List<ServiceRequest> GetServiceRequestByVehicleId(int vehicleId);
    }
}