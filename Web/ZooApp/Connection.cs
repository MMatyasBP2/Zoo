using MongoDB.Driver;
using System.Diagnostics;
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
