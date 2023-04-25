using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Npgsql;
using static System.Int32;

namespace pillar.Repository
{
    public class Tickets
    {
        const string connString = "Host=postgres;Username=postgres;Password=mysecretpassword;Database=pillar";
        
        public static pillar.Ticket.Ticket Add(pillar.Ticket.Ticket ticket)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText =
                            "INSERT INTO tickets (description, status, agent, priority, \"type\", subject, opendate, firstreply, solvedate, reopendate, closeddate, duedate, userid, added, updated)" +
                            "VALUES (@description, @status, @agent, @priority, @type, @subject, @opendate, @firstreply, @solvedate, @reopendate, @closeddate, @duedate, @userid, @added, @updated);" +
                            "select currval('ticket_id_seq');";
                        cmd.Parameters.AddWithValue("description", ticket.Description);
                        cmd.Parameters.AddWithValue("status", ticket.Status);
                        cmd.Parameters.AddWithValue("agent", ticket.Agent);
                        cmd.Parameters.AddWithValue("priority", ticket.Priority);
                        cmd.Parameters.AddWithValue("type", ticket.Type);
                        cmd.Parameters.AddWithValue("subject", ticket.Subject);
                        cmd.Parameters.AddWithValue("opendate", ticket.OpenDate);
                        cmd.Parameters.AddWithValue("firstreply", ticket.FirstReplyDate);
                        cmd.Parameters.AddWithValue("solvedate", ticket.SolveDate);
                        cmd.Parameters.AddWithValue("reopendate", ticket.ReopenDate);
                        cmd.Parameters.AddWithValue("closeddate", ticket.FinalSolveDate);
                        cmd.Parameters.AddWithValue("duedate", ticket.DueDate);
                        cmd.Parameters.AddWithValue("userid", ticket.User.UserId);
                        cmd.Parameters.AddWithValue("added", ticket.AddDate);
                        cmd.Parameters.AddWithValue("updated", ticket.UpdateDate);

                        var dr = cmd.ExecuteReader(); // Will also Execute Insert since it's in the same statement
                        int ticketId;
                        
                        while (dr.Read())
                        {
                            ticketId = dr.GetInt32(0);
                            
                            var newTicket = new pillar.Ticket.Ticket
                            {
                                TicketId = ticketId,
                                Description = ticket.Description,
                                Status = ticket.Status,
                                Agent = ticket.Agent,
                                Priority = ticket.Priority,
                                Type = ticket.Type,
                                Subject = ticket.Subject,
                                OpenDate = ticket.OpenDate,
                                FirstReplyDate = ticket.FirstReplyDate,
                                SolveDate = ticket.SolveDate,
                                ReopenDate = ticket.ReopenDate,
                                FinalSolveDate = ticket.FinalSolveDate,
                                DueDate = ticket.DueDate,
                                //UserId = Parse(dr[13].ToString()),
                                User = new User.User()
                                {
                                    UserId = ticket.User.UserId,
                                    UserName = ticket.User.UserName,
                                    Requester = ticket.User.Requester,
                                    Title = ticket.User.Title,
                                    Organization = ticket.User.Organization,
                                },
                                AddDate = ticket.AddDate,
                                UpdateDate = ticket.UpdateDate,
                            };
                            
                            Console.WriteLine($"Ticket #{ticketId} added on {ticket.OpenDate} for {ticket.Subject}.");
                            conn.Close();
                            var json = JsonConvert.SerializeObject(ticket);
                            Console.WriteLine(json);
                            return newTicket;
                        }
                        conn.Close();
                        return null;
                    }
                }
            }
            catch (NpgsqlException err)
            {
                Console.Write(err);
                return null;
            }
        }

        public static pillar.Ticket.Ticket Retrieve(int ticketId)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Define a query
                var cmd = new NpgsqlCommand("SELECT * FROM tickets t JOIN users u ON t.userid = u.userid JOIN customers c ON c.userid = u.userid WHERE t.ticketid = '" +ticketId+ "'", conn);

                // Execute a query
                var dr = cmd.ExecuteReader();

                // Read all rows and output the first column in each row
                while (dr.Read())
                {
                    var ticket = new pillar.Ticket.Ticket
                    {
                        TicketId = Parse(dr[0].ToString()),
                        Description = dr[1].ToString(),
                        Status = dr[2].ToString(),
                        Agent = dr[3].ToString(),
                        Priority = dr[4].ToString(),
                        Type = dr[5].ToString(),
                        Subject = dr[6].ToString(),
                        OpenDate = Convert.ToDateTime(dr[7].ToString(), null),
                        FirstReplyDate = Convert.ToDateTime(dr[8].ToString(), null),
                        SolveDate = Convert.ToDateTime(dr[9].ToString(), null),
                        ReopenDate = Convert.ToDateTime(dr[10].ToString(), null),
                        FinalSolveDate = Convert.ToDateTime(dr[11].ToString(), null),
                        DueDate = Convert.ToDateTime(dr[12].ToString(), null),
                        //UserId = Parse(dr[13].ToString()),
                        User = new User.User()
                        {
                            UserId = Parse(dr[16].ToString()),
                            UserName = dr[17].ToString(),
                            Requester = dr[23].ToString(),
                            Title = dr[24].ToString(),
                            Organization = dr[29].ToString()
                        },
                        AddDate = Convert.ToDateTime(dr[14].ToString(), null),
                        UpdateDate = Convert.ToDateTime(dr[15].ToString(), null),
                    };
                    var json = JsonConvert.SerializeObject(ticket);
                    Console.WriteLine(json);
                    return ticket;
                }
            }
            return null;
        }
        
         public static List<pillar.Ticket.Ticket> RetrieveByStatus(string status)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    // Define a query
                    var cmd = new NpgsqlCommand("SELECT * FROM tickets t JOIN users u ON t.userid = u.userid JOIN customers c ON c.userid = u.userid WHERE t.status = '" + status + "'", conn);

                    // Execute a query
                    var dr = cmd.ExecuteReader();
                    var tickets = new List<pillar.Ticket.Ticket>();

                    // Read all rows and output the first column in each row
                    while (dr.HasRows)
                    {
                 
                        foreach (var row in dr)
                        {
                            var ticket = new pillar.Ticket.Ticket
                            {
                                TicketId = Parse(dr[0].ToString()),
                                Description = dr[1].ToString(),
                                Status = dr[2].ToString(),
                                Agent = dr[3].ToString(),
                                Priority = dr[4].ToString(),
                                Type = dr[5].ToString(),
                                Subject = dr[6].ToString(),
                                OpenDate = Convert.ToDateTime(dr[7].ToString(), null),
                                FirstReplyDate = Convert.ToDateTime(dr[8].ToString(), null),
                                SolveDate = Convert.ToDateTime(dr[9].ToString(), null),
                                ReopenDate = Convert.ToDateTime(dr[10].ToString(), null),
                                FinalSolveDate = Convert.ToDateTime(dr[11].ToString(), null),
                                DueDate = Convert.ToDateTime(dr[12].ToString(), null),
                                //UserId = Parse(dr[13].ToString()),
                                User = new User.User()
                                {
                                    UserName = dr[17].ToString(),
                                    Requester = dr[23].ToString(),
                                    Title = dr[24].ToString(),
                                    Organization = dr[29].ToString()
                                },
                                AddDate = Convert.ToDateTime(dr[14].ToString(), null),
                                UpdateDate = Convert.ToDateTime(dr[15].ToString(), null),
                            };
                            
                            tickets.Add(ticket);
                        }
                        
                        var ticketsSorted = tickets.OrderByDescending(o=>o.UpdateDate).ToList();

                        var json = JsonConvert.SerializeObject(ticketsSorted);
                        Console.WriteLine(json);
                        return ticketsSorted;
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
         
         public static string RetrieveCountByStatus(string status)
         {
             using (var conn = new NpgsqlConnection(connString))
             {
                 conn.Open();

                 // Define a query
                 var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM tickets WHERE status = '" +status+ "'", conn);
                 
                 // Provide count
                 var count = cmd.ExecuteScalar();
      
                 var json = JsonConvert.SerializeObject(count);
                 Console.WriteLine(json);
                 return json;
             }
             return null;
         }
         
        public static List<pillar.Ticket.Ticket> RetrieveAllByUserId(int userId, string status)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    // Define a query
                    var cmd = new NpgsqlCommand("SELECT * FROM tickets t JOIN users u ON t.userid = u.userid JOIN customers c ON u.userid = c.userid WHERE t.userid ='" +userId+ "' AND t.status = '" +status+ "'", conn);

                    // Execute a query
                    var dr = cmd.ExecuteReader();
                    var tickets = new List<pillar.Ticket.Ticket>();

                    // Read all rows and output the first column in each row
                    while (dr.HasRows)
                    {
                        foreach (var row in dr)
                        {
                            var ticket = new pillar.Ticket.Ticket
                            {
                                TicketId = Parse(dr[0].ToString()),
                                Description = dr[1].ToString(),
                                Status = dr[2].ToString(),
                                Agent = dr[3].ToString(),
                                Priority = dr[4].ToString(),
                                Type = dr[5].ToString(),
                                Subject = dr[6].ToString(),
                                OpenDate = Convert.ToDateTime(dr[7].ToString(), null),
                                FirstReplyDate = Convert.ToDateTime(dr[8].ToString(), null),
                                SolveDate = Convert.ToDateTime(dr[9].ToString(), null),
                                ReopenDate = Convert.ToDateTime(dr[10].ToString(), null),
                                FinalSolveDate = Convert.ToDateTime(dr[11].ToString(), null),
                                DueDate = Convert.ToDateTime(dr[12].ToString(), null),
                                //UserId = Parse(dr[13].ToString()),
                                User = new User.User()
                                {
                                    UserId = Parse(dr[16].ToString()),
                                    UserName = dr[17].ToString(),
                                    Requester = dr[23].ToString(),
                                    Title = dr[24].ToString(),
                                    Organization = dr[29].ToString()
                                },
                                AddDate = Convert.ToDateTime(dr[14].ToString(), null),
                                UpdateDate = Convert.ToDateTime(dr[15].ToString(), null),
                            };
                            
                            tickets.Add(ticket);
                        }

                        var ticketsSorted = tickets.OrderByDescending(o=>o.UpdateDate).ToList();

                        var json = JsonConvert.SerializeObject(ticketsSorted);
                        Console.WriteLine(json);
                        return ticketsSorted;
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
        
        public static List<pillar.Ticket.Ticket> RetrieveAll()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    // Define a query
                    var cmd = new NpgsqlCommand("SELECT * FROM tickets t JOIN users u ON t.userid = u.userid JOIN customers c ON c.userid = u.userid", conn);

                    // Execute a query
                    var dr = cmd.ExecuteReader();
                    var tickets = new List<pillar.Ticket.Ticket>();

                    // Read all rows and output the first column in each row
                    while (dr.HasRows)
                    {
                 
                        foreach (var row in dr)
                        {
                            var ticket = new pillar.Ticket.Ticket
                            {
                                TicketId = Parse(dr[0].ToString()),
                                Description = dr[1].ToString(),
                                Status = dr[2].ToString(),
                                Agent = dr[3].ToString(),
                                Priority = dr[4].ToString(),
                                Type = dr[5].ToString(),
                                Subject = dr[6].ToString(),
                                OpenDate = Convert.ToDateTime(dr[7].ToString(), null),
                                FirstReplyDate = Convert.ToDateTime(dr[8].ToString(), null),
                                SolveDate = Convert.ToDateTime(dr[9].ToString(), null),
                                ReopenDate = Convert.ToDateTime(dr[10].ToString(), null),
                                FinalSolveDate = Convert.ToDateTime(dr[11].ToString(), null),
                                DueDate = Convert.ToDateTime(dr[12].ToString(), null),
                                //UserId = Parse(dr[13].ToString()),
                                User = new User.User()
                                {
                                    UserName = dr[17].ToString(),
                                    Requester = dr[23].ToString(),
                                    Title = dr[24].ToString(),
                                    Organization = dr[29].ToString()
                                },
                                AddDate = Convert.ToDateTime(dr[14].ToString(), null),
                                UpdateDate = Convert.ToDateTime(dr[15].ToString(), null),
                            };
                            
                            tickets.Add(ticket);
                        }

                        var ticketsSorted = tickets.OrderByDescending(o=>o.UpdateDate).ToList();

                        
                        var json = JsonConvert.SerializeObject(ticketsSorted);
                        Console.WriteLine(json);
                        return ticketsSorted;
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
        
        public static string UpdateTicket(pillar.Ticket.Ticket ticket)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string UpdateCmd =
                        "update tickets set description=@description, status=@status, agent=@agent, priority=@priority, \"type\"=@type, subject=@subject, opendate=@opendate, firstreply=@firstreply, solvedate=@solvedate, reopendate=@reopendate, closeddate=@closeddate, duedate=@duedate, userid=@userid, added=@added, updated=@updated where ticketid=@ticketid;";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(UpdateCmd, conn))
                    {
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("description", ticket.Description);
                        cmd.Parameters.AddWithValue("status", ticket.Status);
                        cmd.Parameters.AddWithValue("agent", ticket.Agent);
                        cmd.Parameters.AddWithValue("priority", ticket.Priority);
                        cmd.Parameters.AddWithValue("type", ticket.Type);
                        cmd.Parameters.AddWithValue("subject", ticket.Subject);
                        cmd.Parameters.AddWithValue("opendate", ticket.OpenDate);
                        cmd.Parameters.AddWithValue("firstreply", ticket.FirstReplyDate);
                        cmd.Parameters.AddWithValue("solvedate", ticket.SolveDate);
                        cmd.Parameters.AddWithValue("reopendate", ticket.ReopenDate);
                        cmd.Parameters.AddWithValue("closeddate", ticket.FinalSolveDate);
                        cmd.Parameters.AddWithValue("duedate", ticket.DueDate);
                        cmd.Parameters.AddWithValue("userid", ticket.User.UserId);
                        cmd.Parameters.AddWithValue("ticketid", ticket.TicketId);
                        cmd.Parameters.AddWithValue("added", ticket.AddDate);
                        cmd.Parameters.AddWithValue("updated", ticket.UpdateDate);

                        cmd.ExecuteNonQuery();
                        conn.Close();
                        var json = JsonConvert.SerializeObject(ticket);
                        Console.WriteLine(json);
                        return json;
                    }
                }
            }
            catch (NpgsqlException err)
            {
                Console.Write(err);
                return err.ToString();
            }
        }
    }
}