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

                // ************************* BEFORE DAPPER *************************
                //db.Open();

                //var insertUserCommand = db.CreateCommand();
                //insertUserCommand.CommandText = @"Insert into users (username, password)
                //                            Output inserted.*
                //                            Values(@username, @password)";

                //insertUserCommand.Parameters.AddWithValue("username", username);
                //insertUserCommand.Parameters.AddWithValue("password", password);

                //var reader = insertUserCommand.ExecuteReader(); //want execute reader now since were outputting id

                //if (reader.Read())
                //{
                //    // at least 1 row if it gets inside this code block
                //    var insertedUusername = reader["username"].ToString();
                //    var insertedPassword = reader["password"].ToString();
                //    var insertedId = (int)reader["Id"];
                //    var newUser = new User(insertedUusername, insertedPassword) { Id = insertedId };

                //    return newUser;
                //}

                // ************************* USING DAPPER *************************
                // QueryFirstOrDefault = query the db for the first record. if there isnt one, give me the default value of the reference type
                // output inserted.* is the select statement for inserts; if you dont have this, then the query will always return null
                // UPDATE STATEMENTS -- either use insert or deleted
                var newUser = db.QueryFirstOrDefault<User>(@"Insert into users (username, password)
                                              Output inserted.*
                                              Values(@username, @password)", //setting dapper command statement 
                        new { username, password, }); // new {} is an anonymous type (same as lines 32 & 33) //setting properties                      
                                                      //object creating an anonymous type with the same names as the properties/parameters?; 
                                                      // creating a new user with the username and password returned from sql
                                                      //purpose of anonymous object is to set the parameters
                if (newUser != null)
                {
                    return newUser;
                }
            }
            throw new Exception("No user was created");
        }

        public IEnumerable<User> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                //db.Open(); // Dapper automatically opens the connection for you

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
                //var users = db.Query<User>("select username, password, id from users"); // <> means generic type and you type in what you want to return

                //return users;

                // ********************** SIMPLER DAPPER **********************
                return db.Query<User>("select username, password, id from users");
            }
        }

        public void DeleteUser(int userId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var parameter = new { Id = userId };
                var sql = "Delete FROM Users WHERE Id = @id";

                var rowsAffected = db.Execute(sql, parameter);

                if (rowsAffected != 1)
                {
                    throw new Exception("it didnt do right");
                }
            }
        }

        public User UpdateUser(User userToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {

                var sql = @"UPDATE users
                            SET username = @username,
                                password = @password
                            WHERE id = @id";

                var rowsAffected = db.Execute(sql, userToUpdate); 
                        //if there are more properties, those will get passed in too, but we are only using username, password, & id

                if (rowsAffected ==1)
                {
                    return userToUpdate;
                }
                
            }
            throw new Exception("Could not update user");
        }
    }
}