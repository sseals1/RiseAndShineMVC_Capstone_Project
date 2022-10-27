using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using RiseAndShine.Models;
using System;


namespace RiseAndShine.Models
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public ServiceRequestRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<ServiceRequest> GetServiceRequestByVehicleId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT sr.Id, sr.CarId, sr.DetailTypeId, sr.ServiceDate, sr.ServiceProviderId, sr.Note,
                             
                                dt.DetailPackageName AS PackageName, dt.PackagePrice AS PackagePrice
                         FROM ServiceRequest sr
                         
                         JOIN DetailType dt ON sr.DetailTypeId = dt.Id

                                WHERE sr.CarId = @carId
                                
                    ";
                    DbUtils.AddParameter(cmd, "@carId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<ServiceRequest> serviceRequests = new List<ServiceRequest>();
                        while (reader.Read())
                        {
                            ServiceRequest serviceRequest = new ServiceRequest
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                DetailTypeId = DbUtils.GetInt(reader, "DetailTypeId"),
                                ServiceDate = (DateTime)DbUtils.GetDateTime(reader, "ServiceDate"),
                                ServiceProviderId = DbUtils.GetInt(reader, "ServiceProviderId"),
                                Note = DbUtils.GetString(reader, "Note"),
                                Package = new PackageType()
                                {
                                    Name = DbUtils.GetString(reader, "PackageName"),
                                    Price = DbUtils.GetDecimal(reader, "PackagePrice"),
                                }
                            };

                            serviceRequests.Add(serviceRequest);
                        }

                        return serviceRequests;
                    }
                }
            }
        }

        public ServiceRequest GetServiceRequestById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT sr.Id, sr.CarId, sr.DetailTypeId, sr.ServiceDate, sr.ServiceProviderId, sr.Note
                             
                         FROM ServiceRequest sr

                                WHERE sr.Id = @id
                                
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            ServiceRequest serviceRequest = new ServiceRequest
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                DetailTypeId = DbUtils.GetInt(reader, "DetailTypeId"),
                                ServiceDate = (DateTime)DbUtils.GetDateTime(reader, "ServiceDate"),
                                ServiceProviderId = DbUtils.GetInt(reader, "ServiceProviderId"),
                                Note = DbUtils.GetString(reader, "Note"),

                            };
                            return serviceRequest;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }


        public void UpdateServiceRequest(ServiceRequest serviceRequest)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE ServiceRequest
                           
                         SET   DetailTypeId = @ServiceTypeId,
                               ServiceDate = @ServiceDate,                               
                               Note = @Note
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", serviceRequest.Id);
                    DbUtils.AddParameter(cmd, "@DetailTypeId", serviceRequest.DetailTypeId);
                    DbUtils.AddParameter(cmd, "@DateCreated", serviceRequest.ServiceDate);
                    DbUtils.AddParameter(cmd, "@Note", serviceRequest.Note);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}