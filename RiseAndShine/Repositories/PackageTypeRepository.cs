using Amazon.DirectConnect.Model;
using Microsoft.Data.SqlClient;
using RiseAndShine.Models;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;

namespace RiseAndShine.Repositories
{
    public class PackageTypeRepository : IPackageTypeRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public PackageTypeRepository(IConfiguration config)
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
        public List<PackageType> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                              SELECT  DetailPackageName AS PackageName, PackagePrice AS PackagePrice
                                  FROM DetailType                   
                    ";               

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<PackageType> packageTypes = new List<PackageType>();
                        while (reader.Read())
                        {
                            PackageType packageTypeObj = new PackageType
                            {
                                Name = DbUtils.GetString(reader, "PackageName"),
                                Price = DbUtils.GetDecimal(reader, "PackagePrice"),
                            };

                            packageTypes.Add(packageTypeObj);
                        }

                        return packageTypes;
                    }
                }
            }
        }

    }
}


