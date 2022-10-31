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

        

        public List<ServiceRequest> GetAllAvailableServiceRequests()
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
                                WHERE sr.CarId IS NULL
                    ";
                   
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
                                PackageType = new PackageType()
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

        public List<ServiceRequest> GetAllServiceRequestsWithCarId()
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
                                WHERE sr.CarId IS NOT NULL
                    ";
                    //cmd.Parameters.AddWithValue("@CarId", carId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<ServiceRequest> serviceRequests = new List<ServiceRequest>();
                        while (reader.Read())
                        {
                            ServiceRequest serviceRequest = new ServiceRequest
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                CarId = DbUtils.GetInt(reader, "CarId"),
                                DetailTypeId = DbUtils.GetInt(reader, "DetailTypeId"),
                                ServiceDate = (DateTime)DbUtils.GetDateTime(reader, "ServiceDate"),
                                ServiceProviderId = DbUtils.GetInt(reader, "ServiceProviderId"),
                                Note = DbUtils.GetString(reader, "Note"),
                                PackageType = new PackageType()
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

        public List<ServiceRequest> GetServiceRequestsByVehicleId(int id)
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
                                PackageType = new PackageType()
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

        public ServiceRequest GetServiceRequestByCarId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT sr.Id, sr.CarId, sr.DetailTypeId, sr.ServiceDate, sr.ServiceProviderId, sr.Note,
                             
                                dt.Id AS PackageId, dt.DetailPackageName AS PackageName, dt.PackagePrice AS PackagePrice
                         FROM ServiceRequest sr
                         
                         JOIN DetailType dt ON sr.DetailTypeId = dt.Id

                                WHERE sr.CarId = @carId
                                
                    ";
                    DbUtils.AddParameter(cmd, "@carId", id);

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
                                PackageType = new PackageType()
                                {
                                    Id = DbUtils.GetInt(reader, "PackageId"),
                                    Name = DbUtils.GetString(reader, "PackageName"),
                                    Price = DbUtils.GetDecimal(reader, "PackagePrice"),
                                }
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

        public void Add(ServiceRequest serviceRequest)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO ServiceRequest
                           
                         SET   CarId = @CarId,
                               ServiceProviderId = @ServiceProviderId,
                               DetailTypeId = @DetailTypeId,
                               ServiceDate = @ServiceDate,
                               Note = @Note
                         WHERE Id = @Id"; ;

                    DbUtils.AddParameter(cmd, "@Id", serviceRequest.Id);
                    DbUtils.AddParameter(cmd, "@CarId", serviceRequest.CarId);
                    DbUtils.AddParameter(cmd, "@ServiceProviderId", serviceRequest.ServiceProviderId);
                    DbUtils.AddParameter(cmd, "@DetailTypeId", serviceRequest.DetailTypeId);
                    DbUtils.AddParameter(cmd, "@ServiceDate", serviceRequest.ServiceDate);
                    DbUtils.AddParameter(cmd, "@Note", serviceRequest.Note);

                    serviceRequest.Id = (int)cmd.ExecuteScalar();

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
                           
                         SET   DetailTypeId = @DetailTypeId,
                               ServiceDate = @ServiceDate,
                               Note = @Note
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", serviceRequest.Id);
                    DbUtils.AddParameter(cmd, "@DetailTypeId", serviceRequest.PackageType.Id);
                    DbUtils.AddParameter(cmd, "@ServiceDate", serviceRequest.ServiceDate);
                    DbUtils.AddParameter(cmd, "@Note", serviceRequest.Note);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Update(ServiceRequest serviceRequest)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE ServiceRequest
                           
                         SET    CarId = @CarId,
                                Note = @Note,
                                DetailTypeId = @DetailTypeId
                                
                         WHERE CarId = @Id";

                    
                    DbUtils.AddParameter(cmd, "@CarId", serviceRequest.CarId);
                    DbUtils.AddParameter(cmd, "@Id", serviceRequest.ServiceProviderId);
                    DbUtils.AddParameter(cmd, "@DetailTypeId", serviceRequest.DetailTypeId);
                    DbUtils.AddParameter(cmd, "@Note", serviceRequest.Note);
                   


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteServiceRequest(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM ServiceRequest WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}