using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Conventions;
using ZooApp.Models;

namespace ZooApp.Controllers
{
    public class AnimalController : Controller
    {
        // GET: AnimalController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AnimalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnimalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnimalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnimalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnimalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AnimalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnimalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Connection.ConnectToMongoService();
                Connection.AnimalCollection = Connection.Db.GetCollection<Animal>("animal");

                // Generating ID

                Connection.AnimalCollection.InsertOneAsync(new Animal
                {
                    Id = GenerateRandomId(24),
                    Name = collection["Name"],
                    Racial = collection["Racial"],
                    Description = collection["Description"],
                    Habitat = collection["Habitat"],
                    User = collection["User"]
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private static Random random = new Random();
        private object GenerateRandomId(int v)
        {
            string strarr = "abcdefghijklmnopqrstuvwxyz123456789";
            return new string(Enumerable.Repeat(strarr, v).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
