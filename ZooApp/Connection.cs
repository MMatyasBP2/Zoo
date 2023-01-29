using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ZooApp.Models;

namespace ZooApp
{
    public class Connection
    {
        public static IMongoClient Client { get; set; }
        public static IMongoDatabase Db { get; set; }
        public static string MongoConnection = "mongodb+srv://MMatyas:zooadmin@zoo.aprkucs.mongodb.net/?retryWrites=true&w=majority";
        public static string MongoDatabase = "Zoo";

        public static IMongoCollection<Animal> AnimalCollection { get; set; }
        public static IMongoCollection<Site> SiteCollection { get; set; }
        public static IMongoCollection<Employee> EmployeeCollection { get; set; }
        public static IMongoCollection<Food> FoodCollection { get; set; }
        public static IMongoCollection<Habitat> HabitatCollection { get; set; }
        public static IMongoCollection<User> UserCollection { get; set; }

        internal static void ConnectToMongoService()
        {
            try
            {
                Client = new MongoClient(MongoConnection);
                Db = Client.GetDatabase(MongoDatabase);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}