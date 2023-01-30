using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZooApp.Models;

namespace ZooApp.Controllers
{
    public class FoodController : Controller
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
            Connection.FoodCollection = Connection.Db.GetCollection<Food>("food");
        }
        // GET: Food
        public ActionResult Index()
        {
            GetConnection();
            var filter = Builders<Food>.Filter.Ne("Id", "");
            var result = Connection.FoodCollection.Find(filter).ToEnumerable();
            return View(result);
        }

        // GET: Food/Details/5
        public ActionResult Details(string id)
        {
            GetConnection();
            var filter = Builders<Food>.Filter.Eq("Id", id);
            var result = Connection.FoodCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // GET: Food/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                GetConnection();

                Object id = GenerateRandomId(24);

                Connection.FoodCollection.InsertOneAsync(new Food
                {
                    Id = id,
                    Name = collection["Name"],
                    IsDelicious = bool.Parse(collection["IsDelicious"]),
                    Company = collection["Company"]
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Food/Edit/5
        public ActionResult Edit(string id)
        {
            GetConnection();
            var filter = Builders<Food>.Filter.Eq("Id", id);
            var result = Connection.FoodCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Food/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<Food>.Filter.Eq("Id", id);

                var update = Builders<Food>.Update
                    .Set("Name", collection["Name"])
                    .Set("IsDelicious", bool.Parse(collection["IsDelicious"]))
                    .Set("Company", collection["Company"]);

                var result = Connection.FoodCollection.UpdateOneAsync(filter, update);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Food/Delete/5
        public ActionResult Delete(string id)
        {
            GetConnection();
            var filter = Builders<Food>.Filter.Eq("Id", id);
            var result = Connection.FoodCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Food/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<Food>.Filter.Eq("Id", id);
                var result = Connection.FoodCollection.DeleteOneAsync(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
