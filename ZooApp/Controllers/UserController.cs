using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZooApp.Models;

namespace ZooApp.Controllers
{
    public class UserController : Controller
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
            Connection.UserCollection = Connection.Db.GetCollection<User>("user");
        }
        // GET: User
        public ActionResult Index()
        {
            GetConnection();
            var filter = Builders<User>.Filter.Ne("Id", "");
            var result = Connection.UserCollection.Find(filter).ToEnumerable();
            return View(result);
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            GetConnection();
            var filter = Builders<User>.Filter.Eq("Id", id);
            var result = Connection.UserCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                GetConnection();

                Object id = GenerateRandomId(24);

                Connection.UserCollection.InsertOneAsync(new User
                {
                    Id = id,
                    Name = collection["Name"],
                    Username = collection["Username"],
                    Password = collection["Password"],
                    Sex = char.Parse(collection["Sex"]),
                    Postcode = collection["Postcode"],
                    City = collection["City"],
                    Street = collection["Street"],
                    Number = int.Parse(collection["Number"])
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            GetConnection();
            var filter = Builders<User>.Filter.Eq("Id", id);
            var result = Connection.UserCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<User>.Filter.Eq("Id", id);

                var update = Builders<User>.Update
                    .Set("Name", collection["Name"])
                    .Set("Username", collection["Username"])
                    .Set("Password", collection["Password"])
                    .Set("Sex", char.Parse(collection["Sex"]))
                    .Set("Postcode", collection["Postcode"])
                    .Set("City", collection["City"])
                    .Set("Street", collection["Street"])
                    .Set("Number", int.Parse(collection["Number"]));

                var result = Connection.UserCollection.UpdateOneAsync(filter, update);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            GetConnection();
            var filter = Builders<User>.Filter.Eq("Id", id);
            var result = Connection.UserCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<User>.Filter.Eq("Id", id);
                var result = Connection.UserCollection.DeleteOneAsync(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
