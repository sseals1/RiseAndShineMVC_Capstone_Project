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
                         SELECT sr.Id, sr.DetailTypeId, sr.ServiceDate, sr.ServiceProviderId, sr.Note,
                             
                                dt.DetailPackageName AS PackageName, dt.PackagePrice AS PackagePrice
                         FROM ServiceRequest sr
                         
                         JOIN DetailType dt ON sr.DetailTypeId = dt.Id
                                WHERE sr.Id = @id
                                
                    ";
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<ServiceRequest> ServiceRequests = new List<ServiceRequest>();
                        while (reader.Read())
                        {
                            ServiceRequest serviceRequest = new ServiceRequest
                            {
                                Id = DbUtils.GetInt(reader, "Id"),                              
                                DetailTypeId = DbUtils.GetInt(reader, "DetailTypeId"),
                                ServiceDate = (DateTime)DbUtils.GetDateTime(reader, "ServiceDate"),
                                ServiceProviderId = DbUtils.GetInt(reader, "ServiceProviderId"),
                                Note = DbUtils.GetString(reader, "Note"),

                                //Address = reader.GetString(reader.GetOrdinal("Address")),
                                //UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                //UserType = new UserType()
                                //{
                                //    Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                //    Name = reader.GetString(reader.GetOrdinal("UserTypeName")),
                                //}
                                //UserProfile = new UserProfile()
                                //{
                                //    FirstName = DbUtils.GetString(reader, "FirstName"),
                                //    LastName = DbUtils.GetString(reader, "LastName"),
                                //},
                                Package = new PackageType()
                                {
                                     Name = DbUtils.GetString(reader, "PackageName"),
                                    Price = DbUtils.GetDecimal(reader, "PackagePrice"),
                                }
                            };

                            ServiceRequests.Add(serviceRequest);
                        }

                        return ServiceRequests;
                    }
                }
            }
        }
    }
}