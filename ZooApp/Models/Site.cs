using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class Site
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public float Area { get; set; }
        public string OpeningHours { get; set; }
    }
}