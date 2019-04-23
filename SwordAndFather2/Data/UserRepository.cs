using System.Collections.Generic;
using System.Data.SqlClient;
using SwordAndFather2.Models;

namespace SwordAndFather2.Data
{
    public class UserRepository
    {
        static List<User> _users = new List<User>();

        public User AddUser(string username, string password)
        {
            var newUser = new User(username, password);

            newUser.Id = _users.Count + 1;

            _users.Add(newUser);

            return newUser;
        }

        public List<User> GetAll()
        {
            var users = new List<User>();
            var connection = new SqlConnection("Server=localhost;Database=Sword&Father;Trusted_Connection=True;");
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = "select * from users"; //what the command is going to execute on the server

            var reader = getAllUsersCommand.ExecuteReader();

            while (reader.Read()) // how we get user info out
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