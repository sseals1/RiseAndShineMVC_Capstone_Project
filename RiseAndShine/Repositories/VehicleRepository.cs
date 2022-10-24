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
        private Vehicle vehicle;


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

        public List<Vehicle> GetAllVehicles(int ownerId)
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

                    using SqlDataReader reader = cmd.ExecuteReader();
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

        public List<Vehicle> GetVehicleByOwnerId(int id)
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
                       List<Vehicle> vehicles = new List<Vehicle>();
                        while (reader.Read())
                        {
                            Vehicle vehicle = new Vehicle 
                            {
                                Id =  DbUtils.GetInt(reader, "Id"),
                                Make = DbUtils.GetString(reader, "Make"),
                                Model = DbUtils.GetString(reader, "Model"),
                                Color = DbUtils.GetString(reader, "Color"),
                                ManufactureDate = DbUtils.GetDateTime(reader, "ManufactureDate"),                              
                            };

                           vehicles.Add(vehicle);
                        }     
                        reader.Close();
                        return vehicles;
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
                                        INSERT INTO Car
                                            (Make, Model, Color, ManufactureDate, OwnerId) 
                                        OUTPUT INSERTED.ID
                                            VALUES(@make, @model, @color, @manufactureDate, @ownerId)";

                    DbUtils.AddParameter(cmd, "@ownerId", vehicle.OwnerId);
                    DbUtils.AddParameter(cmd, "@make", vehicle.Make);
                    DbUtils.AddParameter(cmd, "@model", vehicle.Model);
                    DbUtils.AddParameter(cmd, "@color", vehicle.Color);
                    DbUtils.AddParameter(cmd, "@manufactureDate", vehicle.ManufactureDate);

                    vehicle.Id = (int)cmd.ExecuteScalar();

                }
            }
        }

    }
}

