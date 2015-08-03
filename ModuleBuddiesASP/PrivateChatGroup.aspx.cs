using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ModuleBuddiesASP.HelperClasses;

namespace ModuleBuddiesASP
{
    public partial class PrivateChatGroup : System.Web.UI.Page
    {
        HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            HelperClasses.Token myToken = new HelperClasses.Token();

            if (myToken.getToken() == "")
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                try
                {
                    string url = Request.Url.AbsoluteUri;
                    int index = url.IndexOf("?");
                    int uidIndex = url.IndexOf("&uid=");
                    string title = url.Substring(index + 5, uidIndex - (index + 5));
                    title = title.Replace("%20", " ");
                    publicChatLabel.Text = title;
                    docTitleLabel.Text = title;
                }
                catch { }
            }
        }

        protected void addFriendButton_Click(object sender, EventArgs e)
        {
          
            string userName = userInfo.getUserName();
            string userID = userInfo.getUserID();

            string url = Request.Url.AbsoluteUri;
            int gidIndex = url.IndexOf("?gid=") + 5;
            int uidIndex = url.IndexOf("&uid=");
           
            string groupName = url.Substring(gidIndex, uidIndex - gidIndex);
            groupName = groupName.Replace("+", " ");
            string uniqueID = url.Substring(uidIndex+5);

            Boolean notFriend = true;

            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT uniqueID, memberID FROM PrivateChatList");
            String htmlCode = String.Empty;
           
            

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            conn.Open();

            foreach (int i in friendListBox.GetSelectedIndices())
            {
                foreach (DataRow _DataRow in _DataTable.Rows)
                {
                    if (_DataRow["memberID"].ToString() == friendListBox.Items[i].Value)
                    {
                        notFriend = false;
                        break;
                    }

                }
                if (notFriend == true)
                {
                    string insertFriend = "INSERT into PrivateChatList(UniqueID, GroupName, MemberName, MemberID) VALUES('"
                        + uniqueID + "','" + groupName + "','" + friendListBox.Items[i].Text + "','" + friendListBox.Items[i].Value + "');";
                    SqlCommand cmdFriend = new SqlCommand(insertFriend, conn);
                    cmdFriend.ExecuteNonQuery();
                }

                notFriend = true;
            }

            usersListBox.DataBind();

            usersListBox.DataBind();

            string newUrl = url.Substring(0, gidIndex) +
               groupName.Replace("+", "%20") + "&uid=" + uniqueID;

            Response.Redirect(newUrl);
            
        }
        protected void deleteButton_Click(object sender, EventArgs e)
        {
         
            string url = Request.Url.AbsoluteUri;
            int gidIndex = url.IndexOf("?gid=") + 5;
            int uidIndex = url.IndexOf("&uid=");

            string groupName = url.Substring(gidIndex, uidIndex - gidIndex);
            string uniqueID = url.Substring(uidIndex + 5);

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            conn.Open();

            foreach (int i in usersListBox.GetSelectedIndices())
            {
                string delete = "DELETE FROM PrivateChatList WHERE memberID='" + usersListBox.Items[i].Value +
                    "' AND memberName='" + usersListBox.Items[i].Text + "' AND uniqueId='" + uniqueID + "';";

                SqlCommand cmdFriend = new SqlCommand(delete, conn);
                cmdFriend.ExecuteNonQuery();
            }

            usersListBox.DataBind();

            string newUrl = url.Substring(0, gidIndex)  + 
                groupName.Replace("+", "%20") + "&uid=" + uniqueID;

            Response.Redirect(newUrl);
        }

     
    }
}