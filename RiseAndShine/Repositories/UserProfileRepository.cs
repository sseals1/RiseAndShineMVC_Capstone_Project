using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using RiseAndShine.Models;
using System;

namespace RiseAndShine.Models
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public UserProfileRepository(IConfiguration config)
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

        public List<UserProfile> GetAllUserProfiles()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT up.FirebaseUserId, up.Id, up.[FirstName], up.LastName, up.Email, up.Phone, up.Address, up.UserTypeId,
                        ut.Name AS UserTypeName, 
                        c.Make, c.Model, c.Color, c.ImageUrl, c.ManufactureDate,
                        sr.Note,
                        d.DetailPackageName, d.PackagePrice
                   FROM UserProfile up

                        JOIN UserType ut ON ut.Id = up.UserTypeId
                        LEFT JOIN Car c ON c.OwnerId = up.Id
                        LEFT JOIN ServiceRequest sr ON sr.CarId = c.Id
                        LEFT JOIN DetailType d ON d.Id = sr.DetailTypeId
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserProfile> UserProfiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            UserProfile UserProfile = new UserProfile
                            {
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                UserType = new UserType()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                    Name = reader.GetString(reader.GetOrdinal("UserTypeName")),
                                }
                            };

                            UserProfiles.Add(UserProfile);
                        }

                        return UserProfiles;
                    }
                }
            }
        }


        public List<UserProfile> GetAllOwners()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                   SELECT up.FirebaseUserId, up.Id, up.[FirstName], up.LastName, up.Email, up.Phone, up.Address, up.UserTypeId,
                        ut.[Name] AS UserTypeName, 
                        c.Make, c.Model, c.Color, c.ImageUrl, c.ManufactureDate,
                        sr.Note,
                        d.DetailPackageName, d.PackagePrice
                   FROM UserProfile up

                        JOIN UserType ut ON ut.Id = up.UserTypeId
                        LEFT JOIN Car c ON c.OwnerId = up.Id
                        LEFT JOIN ServiceRequest sr ON sr.CarId = c.Id
                        LEFT JOIN DetailType d ON d.Id = sr.DetailTypeId

                   WHERE ut.[Name] = 'Owner'
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserProfile> UserProfiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            UserProfile UserProfile = new UserProfile
                            {
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                UserType = new UserType()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                    Name = reader.GetString(reader.GetOrdinal("UserTypeName")),
                                }
                            };

                            UserProfiles.Add(UserProfile);
                        }

                        return UserProfiles;
                    }
                }
            }
        }
        public UserProfile GetUserProfileById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.FirebaseUserId, up.Id, up.[FirstName], up.LastName, up.Email, up.Phone, up.Address, up.UserTypeId,
                        ut.Id AS UserTypeId, ut.Name AS UserTypeName 
                   FROM UserProfile up
                        JOIN UserType ut ON ut.Id = up.UserTypeId

                   WHERE up.Id = @id         
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UserProfile UserProfile = new UserProfile
                            {
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                UserType = new UserType()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                    Name = reader.GetString(reader.GetOrdinal("UserTypeName")),
                                },
                            };
                            return UserProfile;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public List<UserProfile> GetUserProfileByServiceProviderId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, up.[FirstName], up.LastName, up.Email, up.Phone, up.Address, up.UserTypeId                      
                   FROM UserProfile up

                   WHERE up.Id = @id         
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserProfile> UserProfiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            UserProfile UserProfile = new UserProfile
                            {

                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                               
                            };
                            UserProfiles.Add(UserProfile);
                        }
                        return UserProfiles;
                    }
                }
            }
        }
        public UserProfile GetByFirebaseUserId(string FirebaseUserId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                   SELECT up.FirebaseUserId, up.Id, up.[FirstName], up.LastName, up.Email, up.Phone, up.Address, up.UserTypeId,
                        ut.Id AS UserTypeId, ut.Name AS UserTypeName 
                   FROM UserProfile up
                        JOIN UserType ut ON ut.Id = up.UserTypeId

                   WHERE FirebaseUserId = @firebaseUserId
                    ";

                    DbUtils.AddParameter(cmd, "@firebaseUserId", FirebaseUserId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            UserProfile UserProfile = new UserProfile
                            {
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                UserType = new UserType()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                                    Name = reader.GetString(reader.GetOrdinal("UserTypeName")),
                                },


                            };

                            return UserProfile;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void Add(UserProfile userProfile)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO UserProfile
                                            (FirebaseUserId, Email, UserTypeId, FirstName, LastName, Phone, Address) 
                                        OUTPUT INSERTED.ID
                                            VALUES(@firebaseUserId, @email, @userTypeId, @firstName, @lastName, @phone, @address)";

                    //DbUtils.AddParameter(cmd, "@id", userProfile.Id);
                    DbUtils.AddParameter(cmd, "@firebaseUserId", userProfile.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@firstName", userProfile.FirstName);
                    DbUtils.AddParameter(cmd, "@lastName", userProfile.LastName);
                    DbUtils.AddParameter(cmd, "@phone", userProfile.Phone);
                    DbUtils.AddParameter(cmd, "@address", userProfile.Address);
                    DbUtils.AddParameter(cmd, "@userTypeId", userProfile.UserTypeId);



                    userProfile.Id = (int)cmd.ExecuteScalar();

                }
            }
        }


    }
}
