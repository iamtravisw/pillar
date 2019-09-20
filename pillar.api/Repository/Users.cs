using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Npgsql;
using pillar.User;

namespace pillar.Repository
{
    public class Users
    {
        const string connString = "Host=localhost;Username=postgres;Password=Pepper;Database=pillar";

        public static string Add(pillar.User.User user, string hashedpass)
        {
            var stringAdminValue = user.Admin ? "Y" : "N";
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText =
                            "INSERT INTO users (username, hashedpass, admin, added, updated)" +
                            "VALUES (@username, @hashedpass, @admin, @added, @updated);" +
                            "select currval('users_id_seq');";
                        cmd.Parameters.AddWithValue("username", user.UserNameLowerCase());
                        cmd.Parameters.AddWithValue("hashedpass", hashedpass);
                        cmd.Parameters.AddWithValue("admin", stringAdminValue);
                        cmd.Parameters.AddWithValue("added", user.AddDate);
                        cmd.Parameters.AddWithValue("updated", user.UpdateDate);

                        var reader = cmd.ExecuteReader();
                        int userid;

                        while (reader.Read())
                        {
                            userid = reader.GetInt32(0);
                            Console.WriteLine($"User #{user.UserName} created as userid #{userid}.");
                            conn.Close();
                            
                            var boolAdminValue = stringAdminValue == "Y";

                            var newUser = new pillar.User.User()
                            {
                                UserName = user.UserName,
                                Password = hashedpass,
                                Organization = user.Organization,
                                Requester = user.Requester,
                                Title = user.Title,
                                PrimaryContact = user.PrimaryContact,
                                Admin = boolAdminValue,
                                AddDate = user.AddDate,
                                UpdateDate =  user.UpdateDate
                                
                            };

                            var stringPrimaryContactValue = user.Admin ? "Y" : "N";

                            using (var cmdCustomers = new NpgsqlCommand())
                            {
                                conn.Open();
                                cmdCustomers.Connection = conn;
                                cmdCustomers.CommandText =
                                    "INSERT INTO customers (requester, title, primarycontact, userid, added, updated, organization)" +
                                    "VALUES (@requester, @title, @primarycontact, @userid, @added, @updated, @organization);";
                                cmdCustomers.Parameters.AddWithValue("requester", user.Requester);
                                cmdCustomers.Parameters.AddWithValue("title", user.Title);
                                cmdCustomers.Parameters.AddWithValue("primarycontact", stringPrimaryContactValue);
                                cmdCustomers.Parameters.AddWithValue("userid", userid);
                                cmdCustomers.Parameters.AddWithValue("added", user.AddDate);
                                cmdCustomers.Parameters.AddWithValue("updated", user.UpdateDate);
                                cmdCustomers.Parameters.AddWithValue("organization", user.Organization);
                                cmdCustomers.ExecuteReader();
                            }
                            var json = JsonConvert.SerializeObject(newUser);
                            Console.WriteLine(json);
                            return json;
                        }

                        conn.Close();
                        return null;
                    }
                }
            }
            catch (NpgsqlException err)
            {
                Console.Write(err);
                return err.ToString();
            }
        }

        public static Dictionary<string, string> Verify(string userName)
        {
           Dictionary<string, string> userHash = new Dictionary<string, string>();

            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    // Define a query
                    var cmd = new NpgsqlCommand("SELECT * FROM users WHERE username = '" + userName + "'", conn);

                    // Execute a query
                    var dr = cmd.ExecuteReader();
                    
                    // Read all rows and output the first column in each row
                    while (dr.Read())
                    {

                        userHash.Add("userId", dr[0].ToString());
                        userHash.Add("hashedPass", dr[2].ToString());
                        userHash.Add("admin", dr[3].ToString());

                        Console.WriteLine(userHash);
                        return userHash;
                    }
                }

                return null;
            }

            catch (NpgsqlException err)
            {
                Console.Write(err);
                return null;
            }
        }
        
        
        public static pillar.User.User Retrieve(int userId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    // Define a query
                    var cmd = new NpgsqlCommand("SELECT * FROM users u JOIN customers c ON u.userid = c.userid WHERE u.userid ='" + userId + "'", conn);

                    // Execute a query
                    var dr = cmd.ExecuteReader();
                    
                    // Read all rows and output the first column in each row
                    while (dr.Read())
                    {
                        /*
                         TicketId = Parse(dr[0].ToString()),
                         Description = dr[1].ToString(),
                        */
                        var user = new pillar.User.User()
                        {
                            UserId = userId,
                            UserName = dr[1].ToString(),
                            Password = null,
                            Organization = dr[13].ToString(),
                            Requester = dr[7].ToString(),
                            Title = dr[8].ToString(),
                            Admin = false,
                            AddDate = Convert.ToDateTime(dr[11].ToString(), null),
                            UpdateDate = Convert.ToDateTime(dr[12].ToString(), null),
                        };
                        
                        return user;
                    }
                }

                return null;
            }

            catch (NpgsqlException err)
            {
                Console.Write(err);
                return null;
            }
        }

        public static List<pillar.User.User> RetrieveAll()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    var users = new List<pillar.User.User>();

                    // Define a query
                    var cmd = new NpgsqlCommand("SELECT * FROM users u JOIN customers c ON u.userid = c.userid", conn);

                    // Execute a query
                    var dr = cmd.ExecuteReader();
                    
                    
                    // Read all rows and output the first column in each row
                    while (dr.HasRows)
                    {
                        foreach (var row in dr)
                        {
                            bool isAdmin = (dr[3].ToString() == "Y");

                            var user = new pillar.User.User()
                            {
                                UserId = Int32.Parse(dr[0].ToString()),
                                UserName = dr[1].ToString(),
                                Password = null,
                                Organization = dr[13].ToString(),
                                Requester = dr[7].ToString(),
                                Title = dr[8].ToString(),
                                Admin = isAdmin,
                                AddDate = Convert.ToDateTime(dr[11].ToString(), null),
                                UpdateDate = Convert.ToDateTime(dr[12].ToString(), null),
                            };
                            users.Add(user);
                        }

                        return users;
                    }
                }

                return null;
            }

            catch (NpgsqlException err)
            {
                Console.Write(err);
                return null;
            }
        }


        public static string Delete(int userId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    // Define a query
                    var cmd = new NpgsqlCommand("DELETE FROM users WHERE userid = '" + userId + "'", conn);
                    var cmdCustomers = new NpgsqlCommand("DELETE FROM customers WHERE userid = '" + userId + "'", conn);

                    // Execute a query
                    cmd.ExecuteNonQuery();
                    cmdCustomers.ExecuteNonQuery();
                    return $"Successfully deleted User #{userId}.";
                }

                return null;
            }

            catch (NpgsqlException err)
            {
                Console.Write(err);
                return err.ToString();
            }
        }
    }
}