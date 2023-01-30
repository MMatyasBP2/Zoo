using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class Animal
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public string Racial { get; set; }
        public string Description { get; set; }
        public string Habitat { get; set; }
        public string User { get; set; }
        public string FeedingTime { get; set; }
    }
}