using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ModuleBuddiesASP.HelperClasses;

namespace ModuleBuddiesASP
{
    public partial class ChatNew : System.Web.UI.Page
    {
        HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            HelperClasses.Token myToken = new HelperClasses.Token();

            if (myToken.getToken() == "")
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void createGroupChatButton_Click(object sender, EventArgs e)
        {
            string userName = userInfo.getUserName();
            string userID = userInfo.getUserID();
            Guid UniqueID = Guid.NewGuid();

            if (groupChatTextBox.Value == "")
            {
                warningLabel1.Visible = true;
                //NewChat_Tab.Attributes["class"] = "tab-pane active";
            }
            else
            {
                //SqlConnection conn = new SqlConnection();
                //conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
                string connString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                string insert = "INSERT INTO PrivateChatList(UniqueID, GroupName, MemberName, memberID) VALUES('"
                   + UniqueID + "', @data1, '" + userName + "','" + userID + "');";

                string insertFriend = "INSERT into PrivateChatList(UniqueID, GroupName, MemberName, memberID) VALUES('"
                        + UniqueID + "', @data2, @data3, @data4);";

                using (SqlConnection con = new SqlConnection(connString))
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = insert;
                    cmd.Parameters.AddWithValue("@data1", groupChatTextBox.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                foreach (int i in friendListBox.GetSelectedIndices())
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    using (SqlCommand cmdFriend = conn.CreateCommand())
                    {
                        cmdFriend.CommandText = insertFriend;
                        cmdFriend.Parameters.AddWithValue("@data2", groupChatTextBox.Value);
                        cmdFriend.Parameters.AddWithValue("@data3", friendListBox.Items[i].Text);
                        cmdFriend.Parameters.AddWithValue("@data4", friendListBox.Items[i].Value);
                        conn.Open();
                        cmdFriend.ExecuteNonQuery();
                    }
                }


                Response.Redirect("Chats.aspx");

            }
        }
        protected void chatFriendButton_Click(object sender, EventArgs e)
        {
            string userID = userInfo.getUserID();
            string userName = userInfo.getUserName();
            string friendID = friendListBox2.SelectedItem.Value;
            string friendName = friendListBox2.SelectedItem.Text;
            Boolean notFriend = true;
            /*
            string cid = "";
            if (String.Compare(userID, friendID) < 0)
                cid = userID + friendID;
            else
                cid = friendID + userID;

            string chatID = cid;
            DataEncryptor keys = new DataEncryptor();
            string encryptChatID = keys.EncryptString(chatID);
            */

            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT userID, friendID FROM individualChatList");
            String htmlCode = String.Empty;

            foreach (DataRow _DataRow in _DataTable.Rows)
            {
                if (_DataRow["friendID"].ToString() == friendID)
                {
                    notFriend = false;
                    break;
                }

            }
            if (notFriend == true)
            {
                string connString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                string insert = "INSERT INTO IndividualChatList(userID, userName, friendID, friendName) VALUES('"
                    + userID + "','" + userName + "','" + friendID + "','" + friendName + "');";

                insert += "INSERT INTO IndividualChatList(userID, userName, friendID, friendName) VALUES('"
                    + friendID + "','" + friendName + "','" + userID + "','" + userName + "');";

                using (SqlConnection con = new SqlConnection(connString))
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = insert;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("MainChat.aspx?uid=" + userID + "&fid=" + friendID);

        }
    }
}