using MongoDB.Driver;
using System.Diagnostics;

namespace ZooApp
{
    public class Connection
    {
        public static IMongoClient Client { get; set; }
        public static IMongoDatabase Db { get; set; }
        public static string MongoConnection = "";
        public static string MongoDatabase = "";

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
