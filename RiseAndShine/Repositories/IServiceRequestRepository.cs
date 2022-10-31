using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace RiseAndShine.Models
{
    public interface IServiceRequestRepository
    {
        SqlConnection Connection { get; }

        ServiceRequest GetServiceRequestById(int id);
        List<ServiceRequest> GetServiceRequestsByVehicleId(int vehicleId);
        void UpdateServiceRequest(ServiceRequest serviceRequest);
        void DeleteServiceRequest(int id);
        List<ServiceRequest> GetAllAvailableServiceRequests();
        List<ServiceRequest> GetAllServiceRequestsWithCarId();
        void Add(ServiceRequest newServiceRequest);
        void Update(ServiceRequest newServiceRequest);
        ServiceRequest GetServiceRequestByCarId(int id);
    }
}