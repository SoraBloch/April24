using April24Ajax.Data;
using April24Ajax.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace April24Ajax.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllPeople()
        {
            var peopleRepo = new PeopleRepo(_connectionString);
            List<Person> people = peopleRepo.GetAllPeople();
            return Json(people);
        }
        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PeopleRepo(_connectionString);
            repo.AddPerson(person);
        }
        public IActionResult GetPersonById(int id)
        {
            var repo = new PeopleRepo(_connectionString);
            
            return Json(repo.GetPersonById(id));
        }
    }
}