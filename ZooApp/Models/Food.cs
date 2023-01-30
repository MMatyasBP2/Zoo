using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class Food
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public bool IsDelicious { get; set; }
        public string Company { get; set; }
    }
}