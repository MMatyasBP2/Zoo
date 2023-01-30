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
    public class EmployeeController : Controller
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
            Connection.EmployeeCollection = Connection.Db.GetCollection<Employee>("employee");
        }
        // GET: Employee
        public ActionResult Index()
        {
            GetConnection();
            var filter = Builders<Employee>.Filter.Ne("Id", "");
            var result = Connection.EmployeeCollection.Find(filter).ToEnumerable();
            return View(result);
        }

        // GET: Employee/Details/5
        public ActionResult Details(string id)
        {
            GetConnection();
            var filter = Builders<Employee>.Filter.Eq("Id", id);
            var result = Connection.EmployeeCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                GetConnection();

                Object id = GenerateRandomId(24);

                Connection.EmployeeCollection.InsertOneAsync(new Employee
                {
                    Id = id,
                    Name = collection["Name"],
                    BirthDate = DateTime.Parse(collection["BirthDate"]),
                    Sex = char.Parse(collection["Sex"]),
                    Site = collection["Site"]
                });

                return RedirectToAction("Index");
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong format!");
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {
            GetConnection();
            var filter = Builders<Employee>.Filter.Eq("Id", id);
            var result = Connection.EmployeeCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<Employee>.Filter.Eq("Id", id);

                var update = Builders<Employee>.Update
                    .Set("Name", collection["Name"])
                    .Set("BirthDate", DateTime.Parse(collection["BirthDate"]))
                    .Set("Sex", char.Parse(collection["Sex"]))
                    .Set("Site", collection["Site"]);

                var result = Connection.EmployeeCollection.UpdateOneAsync(filter, update);

                return RedirectToAction("Index");
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong format!");
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(string id)
        {
            GetConnection();
            var filter = Builders<Employee>.Filter.Eq("Id", id);
            var result = Connection.EmployeeCollection.Find(filter).FirstOrDefault();
            return View(result);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                GetConnection();
                var filter = Builders<Employee>.Filter.Eq("Id", id);
                var result = Connection.EmployeeCollection.DeleteOneAsync(filter);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
