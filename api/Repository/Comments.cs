using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Npgsql;
using static System.Int32;

namespace pillar.Repository
{
    public class Comments
    {
        const string connString = "Host=postgres;Username=postgres;Password=mysecretpassword;Database=pillar";
        
        public static Comment.Comment Add(pillar.Comment.Comment comment)
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
                            "INSERT INTO comments (ticketid, userid, message, posteddate, added, updated)" +
                            "VALUES (@ticketid, @userid, @message, @posteddate, @added, @updated);" +
                            "select currval('comments_id_seq');";
                        cmd.Parameters.AddWithValue("ticketid", comment.TicketId);
                        cmd.Parameters.AddWithValue("userid", comment.UserId);
                        cmd.Parameters.AddWithValue("message", comment.Message);
                        cmd.Parameters.AddWithValue("posteddate", comment.PostedDate);
                        cmd.Parameters.AddWithValue("added", comment.AddDate);
                        cmd.Parameters.AddWithValue("updated", comment.UpdateDate);
                
                        var reader = cmd.ExecuteReader(); 
                        int commentId;

                        while (reader.Read())
                        {
                            commentId = reader.GetInt32(0);
                            var currentComment = new Comment.Comment()
                            {
                                UserId = comment.UserId,
                                TicketId = comment.TicketId,
                                Message = comment.Message,
                                User = new User.User()
                                {
                                    UserName = comment.User.UserName,
                                    Requester =  comment.User.Requester,
                                    Title = comment.User.Title,
                                    Organization = comment.User.Organization
                                },
                                PostedDate = comment.PostedDate,
                                AddDate = comment.AddDate,
                                UpdateDate = comment.UpdateDate,

                            };
                            
                            
                            Console.WriteLine($"Comment #{commentId} added to {comment.TicketId} on {comment.PostedDate}.");
                            conn.Close();
                            var json = JsonConvert.SerializeObject(comment);
                            Console.WriteLine(json);
                            return currentComment;
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
        public static pillar.Comment.Comment Retrieve(int commentId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    // Define a query
                    var cmd = new NpgsqlCommand("SELECT * FROM comments WHERE commentid = '" + commentId + "'", conn);

                    // Execute a query
                    var dr = cmd.ExecuteReader();

                    // Read all rows and output the first column in each row
                    while (dr.Read())
                    {
                        var comment = new pillar.Comment.Comment()
                        {
                            TicketId = Parse(dr[1].ToString()),
                            Message = dr[2].ToString(),
                            PostedDate = Convert.ToDateTime(dr[3].ToString(), null),
                            UserId = Parse(dr[4].ToString()),
                            AddDate = Convert.ToDateTime(dr[5].ToString(), null),
                            UpdateDate = Convert.ToDateTime(dr[6].ToString(), null),
                        };

                        var json = JsonConvert.SerializeObject(comment);
                        Console.WriteLine(json);
                        return comment;
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
        
        public static List<pillar.Comment.Comment> RetrieveAll(int ticketId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    // Define a query
                    var cmd = new NpgsqlCommand("SELECT * FROM comments c JOIN users u ON c.userid = u.userid JOIN customers cu ON c.userid = cu.userid WHERE ticketid = '" + ticketId + "'", conn);

                    // Execute a query
                    var dr = cmd.ExecuteReader();
                    var comments = new List<pillar.Comment.Comment>();

                    // Read all rows and output the first column in each row
                    while (dr.HasRows)
                    {
                 
                        foreach (var row in dr)
                        {
                            var comment = new pillar.Comment.Comment() 
                            {
                            TicketId = Parse(dr[1].ToString()),
                            Message = dr[2].ToString(),
                            PostedDate = Convert.ToDateTime(dr[3].ToString(), null),
                            UserId = Parse(dr[4].ToString()),
                            AddDate = Convert.ToDateTime(dr[5].ToString(), null),
                            UpdateDate = Convert.ToDateTime(dr[6].ToString(), null),
                            User = new User.User()
                            {
                                UserName = dr[8].ToString(),
                                Requester = dr[14].ToString(),
                                Title = dr[15].ToString(),
                                Organization = dr[20].ToString()
                            }
                            };
                            
                            comments.Add(comment);
                        }
                        var commentsSorted = comments.OrderBy(o=>o.UpdateDate).ToList();
                        
                        var json = JsonConvert.SerializeObject(comments);
                        Console.WriteLine(json);
                        return commentsSorted;
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
    }
}