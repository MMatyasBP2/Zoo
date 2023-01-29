using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZooApp.Models;

namespace ZooApp.Controllers
{
    public class HabitatController : Controller
    {
        private static Random random = new Random();
        private object GenerateRandomId(int v)
        {
            string str = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(str, v).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void GetConnection()
        {
            Connection.ConnectToMongoService();
            Connection.HabitatCollection = Connection.Db.GetCollection<Habitat>("habitat");
        }
        // GET: Habitat
        public ActionResult Index()
        {
            GetConnection();
            var filter = Builders<Habitat>.Filter.Ne("Id", "");
            var result = Connection.HabitatCollection.Find(filter).ToList();
            return View(result);
        }

        // GET: Habitat/Details/5
        public ActionResult Details(string id)
        {
            GetConnection();
            FilterDefinition<Habitat> filter = Builders<Habitat>.Filter.Eq("Id", id);
            Habitat result = Connection.HabitatCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // GET: Habitat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Habitat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                GetConnection();

                Object id = GenerateRandomId(24);

                Connection.HabitatCollection.InsertOneAsync(new Habitat
                {
                    Id = id,
                    Name = collection["Name"],
                    Location = collection["Location"],
                    Description = collection["Description"],
                    Capacity = int.Parse(collection["Capacity"]),
                    SiteName = collection["SiteName"]
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Habitat/Edit/5
        public ActionResult Edit(string id)
        {
            GetConnection();
            FilterDefinition<Habitat> filter = Builders<Habitat>.Filter.Eq("Id", id);
            Habitat result = Connection.HabitatCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Habitat/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                FilterDefinition<Habitat> filter = Builders<Habitat>.Filter.Eq("Id", id);

                UpdateDefinition<Habitat> update = Builders<Habitat>.Update
                    .Set("Name", collection["Name"])
                    .Set("Location", collection["Location"])
                    .Set("Desciption", collection["Description"])
                    .Set("Capacity", int.Parse(collection["Capacity"]))
                    .Set("SiteName", collection["SiteName"]);

                Task<UpdateResult> result = Connection.HabitatCollection.UpdateOneAsync(filter, update);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Habitat/Delete/5
        public ActionResult Delete(string id)
        {
            GetConnection();
            var filter = Builders<Habitat>.Filter.Eq("Id", id);
            var result = Connection.HabitatCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Habitat/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<Habitat>.Filter.Eq("Id", id);
                var result = Connection.HabitatCollection.DeleteOneAsync(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
