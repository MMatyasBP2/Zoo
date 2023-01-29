using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using ZooApp.Models;

namespace ZooApp.Controllers
{
    public class AnimalController : Controller
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
            Connection.AnimalCollection = Connection.Db.GetCollection<Animal>("animal");
        }

        // GET: Animal
        public ActionResult Index()
        {
            GetConnection();
            FilterDefinition<Animal> filter = Builders<Animal>.Filter.Ne("Id", "");
            List<Animal> result = Connection.AnimalCollection.Find(filter).ToList();
            return View(result);
        }

        // GET: Animal/Details/5
        public ActionResult Details(string id)
        {
            GetConnection();
            FilterDefinition<Animal> filter = Builders<Animal>.Filter.Eq("Id", id);
            Animal result = Connection.AnimalCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // GET: Animal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Animal/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                GetConnection();

                Object id = GenerateRandomId(24);

                Connection.AnimalCollection.InsertOneAsync(new Animal
                {
                    Id = id,
                    Name = collection["Name"],
                    Racial = collection["Racial"],
                    Description = collection["Description"],
                    Habitat = collection["Habitat"],
                    User = collection["User"]
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Animal/Edit/5
        public ActionResult Edit(string id)
        {
            GetConnection();
            var filter = Builders<Animal>.Filter.Eq("Id", id);
            var result = Connection.AnimalCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Animal/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<Animal>.Filter.Eq("Id", id);

                var update = Builders<Animal>.Update
                    .Set("Name", collection["Name"])
                    .Set("Racial", collection["Racial"])
                    .Set("Description", collection["Description"])
                    .Set("Habitat", collection["Habitat"])
                    .Set("User", collection["User"]);

                var result = Connection.AnimalCollection.UpdateOneAsync(filter, update);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Animal/Delete/5
        public ActionResult Delete(string id)
        {
            GetConnection();
            var filter = Builders<Animal>.Filter.Eq("Id", id);
            var result = Connection.AnimalCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Animal/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<Animal>.Filter.Eq("Id", id);
                var result = Connection.AnimalCollection.DeleteOneAsync(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
