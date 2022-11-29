using Orius.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataLibrary.BuisnessLogic;
using System.Linq;
using PagedList;
using PagedList.Mvc;

namespace Orius.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        // GET: Tickets   
        public ActionResult Index(string search, int? page)
        {
            var data = TicketProcessor.LoadTickets();
            List<TicketModel> tickets = new List<TicketModel>();

            foreach (var row in data)
            {
                tickets.Add(new TicketModel
                {
                    Id = row.Id,
                    Title = row.Title,
                    Description = row.Description,
                    Submitter = row.Submitter,
                    TimeSubmitted = row.DateTime,
                    Status = (TicketStatus)row.Status,
                    Priority = (TicketPriority)row.Priority,
                    Claimer = row.Claimer,
                });
            }    

            var highPriorityCount = tickets.Where(x => x.Priority >= TicketPriority.High).Count();

            ViewData.Add("TicketCount", tickets.Count);
            ViewData.Add("HighPriorityTicketCount", highPriorityCount);


            // Filter Search
            if (search != null)
            {
                tickets = tickets.Where(x => x.Id.ToString() == search || x.Title == search || x.Description == search || x.Status.ToString() == search 
                || x.Submitter.ToString() == search || x.TimeSubmitted.ToString() == search || x.Priority.ToString() == search).ToList();
            }

            tickets = tickets.OrderByDescending(x => x.TimeSubmitted).ToList();

            return View("Index", tickets.ToPagedList(page ?? 1, 5));
        }

        public ActionResult CreateTicket()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTicket(TicketModel model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = TicketProcessor.CreateTicket(model.Title, model.Description, User.Identity.Name, model.TimeSubmitted, (int)model.Priority, (int)model.Status);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult ViewTicket(TicketModel model)
        {
            var data = TicketProcessor.LoadTickets().Where(x => x.Id == model.Id).FirstOrDefault();

            var output = new TicketModel
            {
                Id = data.Id,
                Title = data.Title,
                Description = data.Description,
                Priority = (TicketPriority)data.Priority,
                Status = (TicketStatus)data.Status,
                TimeSubmitted = data.DateTime,
                Claimer = data.Claimer
            };

            return View(output);
        }

        [HttpGet]
        public ActionResult EditTicket(TicketModel model)
        {
            var data = TicketProcessor.LoadTickets().Where(x => x.Id == model.Id).FirstOrDefault();

            if (data == null)
            {
                return RedirectToAction("Index", "Tickets");
            }

            var output = new TicketModel
            {
                Id = data.Id,
                Title = data.Title,
                Description = data.Description,
                Priority = (TicketPriority)data.Priority,
                Status = (TicketStatus)data.Status,
                TimeSubmitted = data.DateTime,
                Claimer = data.Claimer
            };
            
            return View(output);
        }


        [HttpPost]
        [ActionName("EditTicket")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTicket(TicketModel model)
        {
            if (ModelState.IsValid)
            {
                TicketProcessor.UpdateTicket(model.Id, model.Title, model.Description, (int)model.Priority, (int)model.Status, model.Claimer);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult DeleteTicket(TicketModel model)
        {
            if (ModelState.IsValid)
            {
                TicketProcessor.DeleteTicket(model.Id);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult ClaimTicket(TicketModel model)
        {
            if (ModelState.IsValid)
            {
                var id = UserProcessor.LoadUsers().Where(x => x.Username == User.Identity.Name).FirstOrDefault().Id;
                TicketProcessor.ClaimTicket(model.Id, id);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}