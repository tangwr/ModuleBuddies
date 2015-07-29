using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Data.SqlClient;
using System.Data;
using ModuleBuddiesASP.HelperClasses;

namespace ModuleBuddiesASP
{
    public class documentHub : Hub
    {
        public void RegisterChatGroup(String docId ,String uid)
        {
            HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();
            string userID = userInfo.getUserID();

            if (userID == uid)
            {
                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                conn.Open();

                string selectDocID = "SELECT memberID FROM documents WHERE docId='" + docId + "';";
                SqlCommand cmd = new SqlCommand(selectDocID, conn);

                SqlDataReader dataReader = null;

                dataReader = cmd.ExecuteReader();

                string sqlMemberID = null;
                List<String> memberIDList = new List<String>();

                while (dataReader.Read())
                {
                    sqlMemberID = dataReader["memberID"].ToString();
                    memberIDList.Add(sqlMemberID);
                }

                conn.Close();

                foreach (string id in memberIDList)
                {
                    if (uid == id)
                    {
                        Groups.Add(Context.ConnectionId, docId);
                        break;
                    }
                }
            }
       
        }
        public void TypeText(string text, string docId)
        {
            HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();
            string userID = userInfo.getUserID();

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            conn.Open();

            string selectDocID = "SELECT docID FROM documents WHERE memberID='" + userID + "';";
            SqlCommand cmd = new SqlCommand(selectDocID, conn);

            SqlDataReader dataReader = null;

            dataReader = cmd.ExecuteReader();

            string sqlDocID = null;
            List<String> uniqueIDList = new List<String>();

            while (dataReader.Read())
            {
                sqlDocID = dataReader["docID"].ToString();
                uniqueIDList.Add(sqlDocID);
            }

            conn.Close();

            //Clients.All.textTyped(connectionId, text);
            foreach (string id in uniqueIDList)
            {
                if(id == docId)
                {
                    //Clients.Others.textTyped(text);
                    Clients.OthersInGroup(docId).textTyped(text);

                    conn.Open();

                    string update = "UPDATE mydoc SET data =N'" + text + "' WHERE docId='"
                        + docId + "';";

                    SqlCommand updateCmd = new SqlCommand(update, conn);

                    updateCmd.ExecuteNonQuery();

                    conn.Close();
                    break;
                }
            }
            
            //Clients.All.textTyped(text);
        }

    
    }
}