using DataLibrary.BuisnessLogic;
using Orius.Models;
using System.Linq;
using System.Web.Mvc;

namespace Orius.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {
            var tickets = TicketProcessor.LoadTickets();
            var users = UserProcessor.LoadUsers();

            var model = new DashboardViewModel();


            foreach (var user in users)
             {
                var submissions = tickets.Where(x => x.Submitter == user.Username).Count();
                var closes = tickets.Where(x => x.Claimer == user.Id).Count();
                var highPrioCloses = tickets.Where(x => x.Submitter == user.Username && x.Priority >= (int)TicketPriority.High).Count();

                if (submissions > model.TopSubmissions)
                {
                    model.TopSubmissions = submissions;
                    model.TopSubmitter = user.Username;
                }


                if (highPrioCloses > model.HighPriorityCloses)
                {
                    model.HighPriorityCloses = highPrioCloses;
                    model.HighPriorityCloser = user.Username;
                }

                //Report Stats
                model.TotalReports = tickets.Count();
                model.OpenReports = tickets.Where(x => x.Status == (int)TicketStatus.Open).Count();
                model.ClaimedReports = tickets.Where(x => x.Status == (int)TicketStatus.Claimed).Count();
                model.ClosedReports = tickets.Where(x => x.Status == (int)TicketStatus.Closed).Count();
            }



            return View(model);
        }

        [Authorize]
        public ActionResult AdminIndex()
        {
            return View();
        }
    }
}