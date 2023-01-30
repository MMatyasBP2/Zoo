using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class User
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public char Sex { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
    }
}