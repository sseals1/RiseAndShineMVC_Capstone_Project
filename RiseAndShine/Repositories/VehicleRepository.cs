using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using RiseAndShine.Models;
using System;
using RiseAndShine.Auth.Models;

namespace RiseAndShine.Models
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public VehicleRepository(IConfiguration config)
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

        public List<Vehicle> GetAllVehicles()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                             SELECT c.Id, c.Make, c.Model, c.Color, c.ManufactureDate,
            up.FirstName, up.LastName, up.FirebaseUserId
                   FROM Car c
                   JOIN UserProfile up ON up.id = c.OwnerId
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Vehicle> vehicles = new List<Vehicle>();
                        while (reader.Read())
                        {
                            Vehicle Vehicle = new Vehicle
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Make = DbUtils.GetString(reader, "Make"),
                                Model = DbUtils.GetString(reader, "Model"),
                                Color = DbUtils.GetString(reader, "Color"),
                                ManufactureDate = DbUtils.GetDateTime(reader, "ManufactureDate"),

                            };

                            vehicles.Add(Vehicle);
                        }

                        return vehicles;
                    }
                }
            }
        }

        public Vehicle GetVehicleById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT c.Id, c.Make, c.Model, c.Color, c.ManufactureDate
                    
                        FROM Car c
                        
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Vehicle Vehicle = new Vehicle
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Make = DbUtils.GetString(reader, "Make"),
                                Model = DbUtils.GetString(reader, "Model"),
                                Color = DbUtils.GetString(reader, "Color"),
                                ManufactureDate = DbUtils.GetDateTime(reader, "ManufactureDate"),

                            };

                            return Vehicle;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        

        public void Add(Vehicle vehicle)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO Vehicle
                                            (Make, Model, Color, ManufactureDate) 
                                        OUTPUT INSERTED.ID
                                            VALUES(@make, @model, @color, @manufactureDate)";

                    
                    DbUtils.AddParameter(cmd, "@make", vehicle.Make);
                    DbUtils.AddParameter(cmd, "@email", vehicle.Model);
                    DbUtils.AddParameter(cmd, "@firstName", vehicle.Color);
                    DbUtils.AddParameter(cmd, "@lastName", vehicle.ManufactureDate);

                    vehicle.Id = (int)cmd.ExecuteScalar();

                }
            }
        }


    }
}
