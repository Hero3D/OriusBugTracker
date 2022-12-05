using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;

namespace DataLibrary.BuisnessLogic
{
    public static class TicketProcessor
    {
        #region Create 
        public static int CreateTicket(string title, string description, string submitter, DateTime dateTime, int priority, int status)
        {      
            var parameters = new SQLParameter[]
            {
                 new SQLParameter("@Title", title),
                 new SQLParameter("@Description", description),
                 new SQLParameter("@Submitter", submitter),
                 new SQLParameter("@DateTime", dateTime.ToString()),
                 new SQLParameter("@Priority",  priority.ToString()),
                 new SQLParameter("@Status", status.ToString())
            };

            return SQLDataAccess.CallStoredProcedure("dbo.spTicket_Create", parameters);
        }
        #endregion

        #region Read
        public static List<TicketModel> LoadTickets()
        {
            string sql = @"exec dbo.spTickets_GetAll";

            return SQLDataAccess.LoadData<TicketModel>(sql);
        }
        #endregion

        #region Update
        public static int UpdateTicket(int id, string title, string description, int priority, int status, int claimer)
        {
            var parameters = new SQLParameter[]
            {
                new SQLParameter("@Id", id.ToString()),
                new SQLParameter("@Title", title),
                new SQLParameter("@Description", description),
                new SQLParameter("@Priority", priority.ToString()),
                new SQLParameter("@Status", status.ToString()),
                new SQLParameter("@Claimer", claimer.ToString()),
            };

            return SQLDataAccess.CallStoredProcedure("dbo.spTicket_Update", parameters);
        }


        public static int ClaimTicket(int ticketID, int claimerID)
        {
            var parameters = new SQLParameter[]
            {
                new SQLParameter("@ClaimerId", claimerID.ToString()),
                new SQLParameter("@TicketId", ticketID.ToString())
            };

            return SQLDataAccess.CallStoredProcedure("dbo.spTicket_Claim", parameters);
        }
        #endregion

        #region Delete
        public static int DeleteTicket(int id)
        {
            var parameters = new SQLParameter[]
            {
                new SQLParameter("@Id", id.ToString())
            };

            return SQLDataAccess.CallStoredProcedure("dbo.spTicket_Delete", parameters);
        }

        #endregion
    }
}
