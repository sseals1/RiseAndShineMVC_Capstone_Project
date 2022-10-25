﻿using Microsoft.Data.SqlClient;
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

        public List<ServiceRequest> GetServiceRequestByUserId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT sr.Id, sr.CarId, sr.DetailTypeId, sr.ServiceDate, sr.ServiceProviderId, sr.Note,
                                up.FirstName AS FirstName, up.LastName AS LastName,
                                dt.DetailPackageName AS PackageName, dt.PackagePrice AS PackagePrice
                         FROM ServiceRequest sr
                         JOIN UserProfile up ON sr.ServiceProviderId = up.Id
                         JOIN DetailType dt ON sr.DetailTypeId = dt.Id
                                WHERE sr.CarId = @carId
                                ORDER BY up.LastName
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
                                CarId = DbUtils.GetInt(reader, "CarId"),
                                DetailTypeId = DbUtils.GetInt(reader, "DetailTypeId"),
                                ServiceDate = (DateTime)DbUtils.GetDateTime(reader, "ServiceDate"),
                                ServiceProviderId = DbUtils.GetInt(reader, "ServiceProvider"),
                                Note = DbUtils.GetString(reader, "Note"),

                                //Address = reader.GetString(reader.GetOrdinal("Address")),
                                //UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                //UserType = new UserType()
                                //{
                                //    Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                //    Name = reader.GetString(reader.GetOrdinal("UserTypeName")),
                                //}
                                UserProfile = new UserProfile()
                                {
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                },
                                Package = new PackageType()
                                {
                                     Name = DbUtils.GetString(reader, "Name"),
                                    Price = DbUtils.GetInt(reader, "Price"),
                                }
                            };

                            serviceRequests.Add(serviceRequest);
                        }

                        return serviceRequests;
                    }
                }
            }
        }
    }
}