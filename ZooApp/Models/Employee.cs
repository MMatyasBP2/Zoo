using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class Employee
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Sex { get; set; }
        public string Site { get; set; }
    }
}