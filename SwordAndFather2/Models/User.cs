﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather2.Models
{
    public class User
    {
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
