using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using RiseAndShine.Models;
using System;


namespace RiseAndShine.Repositories
{


    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public UserTypeRepository(IConfiguration config)
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

        public List<UserType> GetAllUserTypes()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT *                     
                   FROM UserType
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserType> UserTypes = new List<UserType>();
                        while (reader.Read())
                        {
                            UserType userType = new UserType
                            {

                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),

                            };

                            UserTypes.Add(userType);
                        }

                        return UserTypes;
                    }
                }
            }
        }
    }
}
