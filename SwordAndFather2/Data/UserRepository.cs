using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using SwordAndFather2.Models;

namespace SwordAndFather2.Data
{
    public class UserRepository
    {
        //static List<User> _users = new List<User>();
        const string ConnectionString = "Server=localhost;Database=Sword&Father;Trusted_Connection=True"; //field

        public User AddUser(string username, string password)
        {
            //var newUser = new User(username, password);
            //newUser.Id = _users.Count + 1;
            //_users.Add(newUser);
            //return newUser;

            using (var db = new SqlConnection(ConnectionString)) //IDisposable (using statement is basically a try finally calling IDisposable)
            {    
                db.Open();

                var insertUserCommand = db.CreateCommand();
                insertUserCommand.CommandText = @"Insert into users (username, password)
                                            Output inserted.*
                                            Values(@username, @password)";

                insertUserCommand.Parameters.AddWithValue("username", username);
                insertUserCommand.Parameters.AddWithValue("password", password);
                
                var reader = insertUserCommand.ExecuteReader(); //want execute reader now since were outputting id

                if (reader.Read())
                {
                    // at least 1 row if it gets inside this code block
                    var insertedUusername = reader["username"].ToString();
                    var insertedPassword = reader["password"].ToString();
                    var insertedId = (int)reader["Id"];
                    var newUser = new User(insertedUusername, insertedPassword) { Id = insertedId };

                    return newUser;
                }
            }
            throw new Exception("No user found");     
        }

        public IEnumerable<User> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                db.Open();

                // *********** ADO.NET WAY OF DOING THINGS ***********
                //var getAllUsersCommand = connection.CreateCommand();
                //getAllUsersCommand.CommandText = "select * from users"; //what the command is going to execute on the server

                //var reader = getAllUsersCommand.ExecuteReader();

                //while (reader.Read()) // how we get user info out (reader.read returns a bool)
                //{
                //    var id = (int)reader["id"]; //if the id isnt forced into an into, error is thrown
                //    var username = reader["username"].ToString();
                //    var password = reader["password"].ToString();
                //    var user = new User(username, password) { Id = id };

                //    users.Add(user);
                //}

                // ********************** DAPPER **********************
                var users = db.Query<User>("select username, password, id from users"); // <> means generic type and you type in what you want to return
                
                return users;

            }
        }
    }
}