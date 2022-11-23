using Orius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Orius.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            var personList = new List<PersonModel>();
            personList.Add(new PersonModel {FirstName = "John", LastName = "Doe", Age = 50, IsAlive = true});
            personList.Add(new PersonModel { FirstName = "Jane", LastName = "Doe", Age = 48, IsAlive = true });
            personList.Add(new PersonModel { FirstName = "Lisa", LastName = "Novingham", Age = 35, IsAlive = false });
            personList.Add(new PersonModel { FirstName = "Kermit", LastName = "Frog", Age = 19, IsAlive = true });


            return View(personList);
        }
    }
}