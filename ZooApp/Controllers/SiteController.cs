using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZooApp.Models;

namespace ZooApp.Controllers
{
    public class SiteController : Controller
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
            Connection.SiteCollection = Connection.Db.GetCollection<Site>("site");
        }
        // GET: Site
        public ActionResult Index()
        {
            GetConnection();
            var filter = Builders<Site>.Filter.Ne("Id", "");
            var result = Connection.SiteCollection.Find(filter).ToList();
            return View(result);
        }

        // GET: Site/Details/5
        public ActionResult Details(int id)
        {
            GetConnection();
            var filter = Builders<Site>.Filter.Eq("Id", id);
            var result = Connection.SiteCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // GET: Site/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Site/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                GetConnection();

                Object id = GenerateRandomId(24);

                Connection.SiteCollection.InsertOneAsync(new Site
                {
                    Id = id,
                    Name = collection["Name"],
                    Area = float.Parse(collection["Area"]),
                    OpeningHours = collection["OpeningHours"],
                });

                return RedirectToAction("Index");
            }
            /*catch ()
            {

            }*/
            catch
            {
                return View();
            }
        }

        // GET: Site/Edit/5
        public ActionResult Edit(string id)
        {
            GetConnection();
            var filter = Builders<Site>.Filter.Eq("Id", id);
            var result = Connection.SiteCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Site/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<Site>.Filter.Eq("Id", id);

                var update = Builders<Site>.Update
                    .Set("Name", collection["Name"])
                    .Set("Area", float.Parse(collection["Area"]))
                    .Set("OpeningHours", collection["OpeningHours"]);

                var result = Connection.SiteCollection.UpdateOneAsync(filter, update);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Site/Delete/5
        public ActionResult Delete(string id)
        {
            GetConnection();
            var filter = Builders<Site>.Filter.Eq("Id", id);
            var result = Connection.SiteCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Site/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<Site>.Filter.Eq("Id", id);
                var result = Connection.SiteCollection.DeleteOneAsync(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
