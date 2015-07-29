using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ModuleBuddiesASP.HelperClasses;
using System.Data;

namespace ModuleBuddiesASP
{
    public class GroupHub : Hub
    {
        public void GetChatHistory(String groupName, String uid)
        {
            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT uid, message, timestamp FROM groupchathistory WHERE groupname = '" + groupName + "'");
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
                    strMessage = GetFIDChatString(msg, datetime, _DataRow["uid"].ToString());
                    
                    Clients.Caller.updateChat(strMessage);
                }
            }
        }

        public void Send(String msg, String groupName, String uid, String datetime)
        {

            String strMessage;

            strMessage = GetFIDChatString(msg, datetime, uid);
            //Clients.Others.updateChat(strMessage);
            Clients.OthersInGroup(groupName).updateChat(strMessage);

            strMessage = GetUIDChatString(msg, datetime);
            Clients.Caller.updateChat(strMessage);


            /* Add Message to DB */

            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            _SqlWrapper.executeNonQuery(@"INSERT INTO GroupChatHistory (groupName, uid,message, timestamp) VALUES ('" + groupName + "', '" + uid + "', '" + msg + "', '" + datetime + "')");
        }

        public void RegisterChatGroup(String groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
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

        private String GetFIDChatString(String msg, String datetime, String uid)
        {
            return @"<div class=""direct-chat-msg"">
                <div class='direct-chat-info clearfix'>
                    <span class='direct-chat-name pull-left'>" + uid + @"</span>
                    <span class='direct-chat-timestamp pull-right'>" + datetime + @"</span>
                </div><!-- /.direct-chat-info -->
                <div class=""direct-chat-text"">" + msg +
                @"</div><!-- /.direct-chat-text -->
            </div><!-- /.direct-chat-msg -->";
        }
    }
}