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
    public partial class Chats : System.Web.UI.Page
    {
        HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            HelperClasses.Token myToken = new HelperClasses.Token();

            if (myToken.getToken() == "")
            {
                Response.Redirect("Home.aspx");
            }


            chatGroupListDiv.Controls.Add(new LiteralControl() { Text = getPublicChatListHtml() });
            myChatListDiv.Controls.Add(new LiteralControl() { Text = getMyChatListHtml() });
           
        }

        protected void createGroupChatButton_Click(object sender, EventArgs e)
        {
            string userName = userInfo.getUserName();
            string userID = userInfo.getUserID();
            Guid UniqueID = Guid.NewGuid();

            if(groupChatTextBox.Value == "")
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
                if ( _DataRow["friendID"].ToString() == friendID )
                {
                    notFriend = false;
                    break;
                }
               
            }
            if(notFriend == true)
            {
                string connString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                string insert = "INSERT INTO IndividualChatList(userID, userName, friendID, friendName) VALUES('"
                    + userID + "','" + userName + "','" + friendID + "','" + friendName + "');";

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
        protected void createPublicChatButton_Click(object sender, EventArgs e)
        {
            string userName = userInfo.getUserName();
            string userID = userInfo.getUserID();
            Guid UniqueID = Guid.NewGuid();

            if (publicChatTextBox.Value == "")
            {
                warningLabel2.Visible = true;
                //NewChat_Tab.Attributes["class"] = "tab-pane active";
            }
            else
            {
                string connString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
                string insert1 = "INSERT INTO PublicChatList(UniqueID, GroupName, MemberName, memberID) VALUES('"
                    + UniqueID + "'," + "@data1" + ",'" + userName + "','" + userID + "');";

                string insert2 = "INSERT INTO PublicChatAuthor(UniqueID, GroupName, AuthorID, AuthorName) VALUES('"
                    + UniqueID + "'," + "@data2" + ",'" + userID + "','" + userName + "');";

                using (SqlConnection con = new SqlConnection(connString))
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = insert1;
                    cmd.Parameters.AddWithValue("@data1", publicChatTextBox.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                using (SqlConnection con = new SqlConnection(connString))
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = insert2;
                    cmd.Parameters.AddWithValue("@data2", publicChatTextBox.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
           
                Response.Redirect(Request.RawUrl);


            }
        }

        /*
        protected void CreateChannelBtn_Click(object sender, EventArgs e)
        {
            /*
            String channelName = "";//ChannelNameTextBox.Value;

            if (String.IsNullOrEmpty(channelName) || String.IsNullOrWhiteSpace(channelName))
                return;

            //If channel already exists skip
            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT groupName FROM groupList");

            foreach (DataRow _DataRow in _DataTable.Rows)
            {
                if (_DataRow["groupName"].ToString() == channelName)
                    return;
            }

            //Else proceed to updaate SqlTable
            _SqlWrapper.executeNonQuery(@"INSERT INTO grouplist (groupName) VALUES ('" + channelName + "')");

            //Refresh Page
            HttpContext.Current.Response.Redirect("Test.aspx");
             * 
        } */

        //Public Chat add join
        public String getPublicChatListHtml()
        {
            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT uniqueID, groupName, authorName FROM publicChatAuthor");
            String htmlCode = String.Empty;

            foreach (DataRow _DataRow in _DataTable.Rows)
            {
               
                htmlCode += @"<tr><td>" + _DataRow["groupName"].ToString() + @"</td>"  +
                            @"<td>" + _DataRow["authorName"].ToString() + @"</td>"  
                         + @"<td><a ID=""pubChatGrp"" runat=""server""  style=""cursor: pointer;""
                        onclick=""joinPublicChat('"  + _DataRow["uniqueID"].ToString() + "','" + _DataRow["groupName"].ToString() + @"')"">Join</a> </td></tr>";
            }

            return htmlCode;

        }

        public String getMyChatListHtml()
        {
            string userID = userInfo.getUserID();

            String htmlCode = String.Empty;

            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");

            //Individual List
            DataTable _individualDataTable = _SqlWrapper.executeQuery(@"SELECT friendID, friendName FROM IndividualChatList WHERE userID='" + userID + "'");
            foreach (DataRow _DataRow in _individualDataTable.Rows)
            {
                htmlCode += @"<tr>
                                <td>" + _DataRow["friendName"].ToString() + @"</td>  
                                 <td>Individual</td>          
                                <td><a ID=""indivChatGrp"" runat=""server""  style=""cursor: pointer;""
                                        onclick=""individualChatGroup('" + userID + "','" + _DataRow["friendID"].ToString() + @"')"" >Chat</a></td>     
                                 <td><a ID=""deleteLink"" runat=""server""  style=""cursor: pointer;""
                                        onclick=""deleteChat('individual','" + _DataRow["friendID"].ToString() + @"')"" >Delete</a></td></tr>";
            }

            //Group List
            DataTable _groupDataTable = _SqlWrapper.executeQuery(@"SELECT UniqueID, GroupName FROM PrivateChatList WHERE memberID='" + userID + "'");           
            foreach (DataRow _DataRow in _groupDataTable.Rows)
            {
                htmlCode += @"<tr>
                                <td>" + _DataRow["GroupName"].ToString() + @"</td>  
                                 <td>Group</td>          
                                <td><a ID=""pvtChatGrp"" runat=""server""  style=""cursor: pointer;""
                                        onclick=""privateChatGroup('" + _DataRow["GroupName"].ToString() + "','" + _DataRow["UniqueID"].ToString() + @"')"" >Chat</a></td>     
                                 <td><a ID=""deleteLink"" runat=""server""  style=""cursor: pointer;""
                                        onclick=""deleteChat('group','" + _DataRow["UniqueID"].ToString() + @"')"" >Delete</a></td></tr>";
            }

            
            
            //Public List
            DataTable _publicDataTable = _SqlWrapper.executeQuery(@"SELECT UniqueID, GroupName FROM PublicChatList WHERE memberID='" + userID + "'");
            foreach (DataRow _DataRow in _publicDataTable.Rows)
            {
                htmlCode += @"<tr>
                                <td>" + _DataRow["GroupName"].ToString() + @"</td>  
                                 <td>Public</td>          
                                <td><a ID=""pubChatGrp"" runat=""server""  style=""cursor: pointer;""
                                        onclick=""publicChatGroup('" + _DataRow["GroupName"].ToString() + "','" + _DataRow["UniqueID"].ToString() + @"')"" >Chat</a></td>    
                                <td><a ID=""deleteLink"" runat=""server""  style=""cursor: pointer;""
                                        onclick=""deleteChat('public','" + _DataRow["UniqueID"].ToString() + @"')"" >Delete</a></td></tr>";
            }
            return htmlCode;
        }

        protected void SelectedIndexChanged_PublicName(object sender, EventArgs e)
        {
            
            editPublicNameTextBox.Text = editPublicNameListBox.SelectedItem.Text;
            idPublicLabel.Text = editPublicNameListBox.SelectedItem.Value;
            
        }

        protected void SelectedIndexChanged_GroupName(object sender, EventArgs e)
        {
            editGroupNameTextBox.Text = editGroupNameListBox.SelectedItem.Text;
            idGroupLabel.Text = editGroupNameListBox.SelectedItem.Value;
        }

        protected void editGroupNameButton_Click(object sender, EventArgs e)
        {
            if (editGroupNameTextBox.Text != "")
            {
                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                conn.Open();

                string update = "UPDATE PrivateChatList SET GroupName =N'" + editGroupNameTextBox.Text + "' WHERE UniqueId='" + idGroupLabel.Text + "';";

                update += "UPDATE PrivateChatData SET GroupName =N'" + editGroupNameTextBox.Text + "' WHERE UniqueId='" + idGroupLabel.Text + "';";
              

                SqlCommand updateCmd = new SqlCommand(update, conn);

                updateCmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        protected void editPublicNameButton_Click(object sender, EventArgs e)
        {
            if (editPublicNameTextBox.Text != "")
            {
                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                conn.Open();

                string update = "UPDATE PublicChatList SET GroupName =N'" + editPublicNameTextBox.Text + "' WHERE UniqueId='" + idPublicLabel.Text + "';";

                update += "UPDATE PublicChatData SET GroupName =N'" + editPublicNameTextBox.Text + "' WHERE UniqueId='" + idPublicLabel.Text + "';";
                update += "UPDATE PublicChatAuthor SET GroupName =N'" + editPublicNameTextBox.Text + "' WHERE UniqueId='" + idPublicLabel.Text + "';";

                SqlCommand updateCmd = new SqlCommand(update, conn);

                updateCmd.ExecuteNonQuery();

                conn.Close();
            }

           
        }
        protected void deleteGroupNameButton_Click(object sender, EventArgs e)
        {
            try
            {
                string userID = userInfo.getUserID();

                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                conn.Open();

                string delete = "DELETE FROM PrivateChatList  WHERE UniqueId='" + idGroupLabel.Text + "' AND MemberID='" + userID + "';";

                SqlCommand deleteCmd = new SqlCommand(delete, conn);

                deleteCmd.ExecuteNonQuery();

                conn.Close();
            }
            catch { }
        }

        protected void deletePublicNameButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                conn.Open();

                string delete = "DELETE FROM PublicChatList  WHERE UniqueId='" + idPublicLabel.Text + "';";

                delete += "DELETE FROM PublicChatData  WHERE UniqueId='" + idPublicLabel.Text + "';";
                delete += "DELETE FROM PublicChatAuthor WHERE UniqueId='" + idPublicLabel.Text + "';";

                SqlCommand deleteCmd = new SqlCommand(delete, conn);

                deleteCmd.ExecuteNonQuery();

                conn.Close();
            }
            catch { }
        }

        
    }
}