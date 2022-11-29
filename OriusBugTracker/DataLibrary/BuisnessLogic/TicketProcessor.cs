using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BuisnessLogic
{
    public static class TicketProcessor
    {
        public static int CreateTicket(string title, string description, string submitter, DateTime dateTime, int priority, int status)
        {
            var sql = $@"exec dbo.spTicket_Create @Title = '{title}', @Description = '{description}', @Submitter = '{submitter}', @DateTime = '{dateTime}', @Priority = '{priority}', @Status = '{status}'";

            return SQLDataAccess.ExecuteData(sql);
        }

        public static int UpdateTicket(int id, string title, string description, int priority, int status, int claimer)
        {
            string sql = $@"exec dbo.spTicket_Update @Id = {id}, @Title = '{title}', @Description = '{description}', @Priority = '{priority}', @Status = '{status}', @Claimer = '{claimer}'";

            return SQLDataAccess.ExecuteData(sql);
        }

        public static int DeleteTicket(int id)
        {
            var sql = $"exec dbo.spTicket_Delete {id}";

            return SQLDataAccess.ExecuteData(sql);
        }

        public static int ClaimTicket(int ticketID, int claimerID)
        {
            var sql = $"exec dbo.spTicket_Claim @ClaimerId = {claimerID}, @TicketId = {ticketID}";

            return SQLDataAccess.ExecuteData(sql);
        }

        public static List<TicketModel> LoadTickets()
        {
            string sql = @"exec dbo.spTickets_GetAll";

            return SQLDataAccess.LoadData<TicketModel>(sql);
        }
      
    }
}
