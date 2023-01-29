using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class Habitat
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string SiteName { get; set; }
    }
}