using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ModuleBuddiesASP.HelperClasses;
using System.Data;

namespace ModuleBuddiesASP
{
    public class PublicChatHub : Hub
    {
        public void GetChatHistory(String groupName, String uid, String userInfo)
        {
            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT UniqueID, Msg, TimeStamp, userInfo FROM PublicChatData WHERE UniqueID = '" + uid + "'");
            String strMessage;

            foreach (DataRow _DataRow in _DataTable.Rows)
            {
                String msg = _DataRow["msg"].ToString();
                String datetime = _DataRow["timestamp"].ToString();

                //If this message is sent by me
                if (_DataRow["userInfo"].ToString() == userInfo)
                {
                    strMessage = GetUIDChatString(msg, datetime);

                    Clients.Caller.updateChat(strMessage);
                }
                else
                {
                    strMessage = GetFIDChatString(msg, datetime, _DataRow["UniqueID"].ToString(), _DataRow["userInfo"].ToString());

                    Clients.Caller.updateChat(strMessage);
                }
            }
        }

        public void Send(String msg, String groupName, String uid, String datetime, String userInfo)
        {

            String strMessage;

            strMessage = GetFIDChatString(msg, datetime, uid, userInfo);
            //Clients.Others.updateChat(strMessage);
            Clients.OthersInGroup(uid).updateChat(strMessage);

            strMessage = GetUIDChatString(msg, datetime);
            Clients.Caller.updateChat(strMessage);


            /* Add Message to DB */

            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            _SqlWrapper.executeNonQuery(@"INSERT INTO PublicChatData (UniqueID, groupName, msg, timestamp, userInfo) VALUES ('" + uid + "', '" + groupName + "', '" + msg + "', '" + datetime + "', '" + userInfo + "')");
        }

        public void RegisterChatGroup(String uid)
        {
            Groups.Add(Context.ConnectionId, uid);
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

        private String GetFIDChatString(String msg, String datetime, String uid, String otherUserInfo)
        {
            return @"<div class=""direct-chat-msg"">
                <div class='direct-chat-info clearfix'>
                    <span class='direct-chat-name pull-left'>" + otherUserInfo + @"</span>
                    <span class='direct-chat-timestamp pull-right'>" + datetime + @"</span>
                </div><!-- /.direct-chat-info -->
                <div class=""direct-chat-text"">" + msg +
                @"</div><!-- /.direct-chat-text -->
            </div><!-- /.direct-chat-msg -->";
        }
    }
}