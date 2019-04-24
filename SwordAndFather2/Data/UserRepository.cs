﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            try
            {
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"Insert into users (username, password)
                                            Output inserted.*
                                            Values('{username}', '{password}'";

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
            finally //any code inside finally will also be executed regardless of whether an exception is thrown or not
            {
                connection.Close();
            }


            throw new Exception("No user found");

        }

        public List<User> GetAll()
        {
            var users = new List<User>();
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = "select * from users"; //what the command is going to execute on the server

            var reader = getAllUsersCommand.ExecuteReader();

            while (reader.Read()) // how we get user info out (reader.read returns a bool)
            {
                var id = (int)reader["id"]; //if the id isnt forced into an into, error is thrown
                //var id = reader.GetInt32(0);
                var username = reader["username"].ToString();
                var password = reader["password"].ToString();
                var user = new User(username, password) { Id = id };

                users.Add(user);
            }
            connection.Close();

            return users;
        }
    }
}