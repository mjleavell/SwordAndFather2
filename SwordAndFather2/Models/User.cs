using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather2.Models
{
    public class User
    {
        //public User(string username, string password)
        //{
        //    Username = username;
        //    Password = password;
        //}

        //// no constructor or constructor that takes in everything
        //public User(string username, string password, int id)
        //{
        //    Username = username;
        //    Password = password;
        //    Id = id;
        //}

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Target> Targets { get; set; }
    }
}
