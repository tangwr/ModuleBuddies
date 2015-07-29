using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ModuleBuddiesASP.HelperClasses;
using System.Data;
using System.Data.SqlClient;


namespace ModuleBuddiesASP
{
    public class chatHub : Hub
    {

        public void GetChatHistory(String uid, String fid)
        {
            Chat _Chat = new Chat(uid, fid);
            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT uid, fid, message, timestamp FROM chathistory WHERE chatID = '" + _Chat.ChatID + "'");
            String strMessage;

            foreach (DataRow _DataRow in _DataTable.Rows)
            {
                String msg = _DataRow["message"].ToString();
                String datetime = _DataRow["timestamp"].ToString();

                //If this message is sent by me
                if (_DataRow["uid"].ToString() == uid)
                {
                    strMessage = GetUIDChatString(msg, datetime);

                    Clients.Caller.updateChat(strMessage);
                }
                else
                {
                    strMessage = GetFIDChatString(msg, datetime, uid, fid);

                    Clients.Caller.updateChat(strMessage);
                }
            }
        }

        public void RegisterChatGroup(String uid, String fid)
        {
            Chat _Chat = new Chat(uid, fid);
            Groups.Add(Context.ConnectionId, _Chat.ChatID);
        }

        private String GetUIDChatString(String msg, String datetime)
        {
            return @"<div class=""direct-chat-msg right"">
                <div class='direct-chat-info clearfix'>
                    <span class='direct-chat-name pull-right'>Me</span>
                    <span class='direct-chat-timestamp pull-left'>" + datetime + @"</span>
                </div><!-- /.direct-chat-info -->
                    
                <div class=""direct-chat-text"">" + msg +
                @"</div><!-- /.direct-chat-text -->
            </div>";
        }

        private String GetFIDChatString(String msg, String datetime, String uid, String fid)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            conn.Open();

            string insert = "SELECT(friendName) FROM FriendList WHERE userID='" + uid + "' and friendID='" + fid + "'";
            SqlCommand cmd = new SqlCommand(insert, conn);

            string buddyName  = (string)cmd.ExecuteScalar();

            conn.Close();

            return @"<div class=""direct-chat-msg"">
                <div class='direct-chat-info clearfix'>
                    <span class='direct-chat-name pull-left'>" + buddyName + @"</span>
                    <span class='direct-chat-timestamp pull-right'>" + datetime + @"</span>
                </div><!-- /.direct-chat-info -->
                <div class=""direct-chat-text"">" + msg +
                @"</div><!-- /.direct-chat-text -->
            </div><!-- /.direct-chat-msg -->";
        }
        public void Send(String msg, String uid, String fid, String datetime)
        {
            Chat _Chat = new Chat(uid, fid);
            String strMessage;

            strMessage = GetFIDChatString(msg, datetime, uid, fid);
            //Clients.Others.updateChat(strMessage);
            Clients.OthersInGroup(_Chat.ChatID).updateChat(strMessage);

            strMessage = GetUIDChatString(msg, datetime);
            Clients.Caller.updateChat(strMessage);


            /* Add Message to DB */

            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            _SqlWrapper.executeNonQuery(@"INSERT INTO ChatHistory (chatID, uid, fid, message, timestamp) VALUES ('" + _Chat.ChatID + "', '" + _Chat.UserID + "', '" + _Chat.FriendID + "', N'" + msg + "', '" + datetime + "')");
        }
    }
}