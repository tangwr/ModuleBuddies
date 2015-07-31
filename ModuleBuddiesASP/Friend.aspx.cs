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
    public partial class Friend : System.Web.UI.Page
    {
        HelperClasses.ModuleInfo modInfo = new HelperClasses.ModuleInfo();
        HelperClasses.CourseID course = new HelperClasses.CourseID();
        HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();

        List<String> ListModCode = new List<String>();
        List<String> ListModName = new List<String>();
        List<String> ListCourseSem = new List<String>();
        List<String> ListAcadYear = new List<String>();
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            LinkButton chatLink = (LinkButton)Master.FindControl("chatLinkButton");
            LinkButton logoutLink = (LinkButton)Master.FindControl("logoutLinkButton");
            chatLink.Visible = true;
            logoutLink.Visible = true;
            */

            
            Master.getVisibleLink();

            HelperClasses.Token myToken = new HelperClasses.Token();

            if (myToken.getToken() == "")
            {
                Response.Redirect("Home.aspx");
            }

            ListModCode = modInfo.getModInfo("CourseCode");
            ListModName = modInfo.getModInfo("CourseName");
            ListCourseSem = modInfo.getSemNum(modInfo.getModInfo("CourseSemester"));
            ListAcadYear = modInfo.getModInfo("CourseAcadYear");

            

            /* from test friends */

            //groupListDiv.Controls.Add(new LiteralControl() { Text = moduleFriendList() });
            addFriendToSql();
            
        }
     
        protected void editNameButton_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = userInfo.getUserName();
                string userID = userInfo.getUserID();

                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                conn.Open();


                string insert = "UPDATE FriendList" + " SET friendName='" + editNameTextBox.Text + "' WHERE userID='"
                    + userID + "' AND userName='" + userName + "' AND friendID='" + idLabel.Text + "';";
                SqlCommand cmd = new SqlCommand(insert, conn);


                cmd.ExecuteNonQuery();


                conn.Close();
                //friendListBox.DataBind();
                editNameListBox.DataBind();
               
            }
            catch { }

            warningLabel.Text = "";
            friendIdTextBox.Text = "";
            friendNameTextBox.Text = "";
            editNameTextBox.Text = "";
        }
       
        protected void SelectedIndexChanged(object sender, EventArgs e)
        {          
            editNameTextBox.Text = editNameListBox.SelectedItem.Text;
            idLabel.Text = editNameListBox.SelectedItem.Value;
        }
        

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {

                string userID = userInfo.getUserID();

                //string friendID = friendListBox.SelectedItem.Value;
                string friendID = editNameListBox.SelectedItem.Value;
                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                conn.Open();

                string insert = "DELETE FROM FriendList WHERE userID='" + userID + "' and friendID='" + friendID + "'"
                    + "DELETE FROM FriendList WHERE userID='" + friendID + "' and friendID='" + userID + "'";
                SqlCommand cmd = new SqlCommand(insert, conn);

                cmd.ExecuteNonQuery();

                conn.Close();

                //friendListBox.DataBind();
                editNameListBox.DataBind();
              
            }
            catch { }

            warningLabel.Text = "";
            friendIdTextBox.Text = "";
            friendNameTextBox.Text = "";
            editNameTextBox.Text = "";
        }
        /*
        public void sqlConnection(string friendID, string friendName)
        {


            string userName = userInfo.getUserName();
            string userID = userInfo.getUserID();

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            conn.Open();


            string insert = "INSERT into FriendList(userID,userName,friendID,friendName, isFriend, isAck, isAccepted) " + " VALUES ('" + userID + "', '" + userName + "', '" + friendID + "', '" + friendName + "','False','False','False')"
                + "INSERT into FriendList(userID,userName,friendID,friendName, isFriend, isAck, isAccepted) " + " VALUES ('" + friendID + "', '" + friendName + "', '" + userID + "', '" + userName + "','False','False','False');";
            SqlCommand cmd = new SqlCommand(insert, conn);


            cmd.ExecuteNonQuery();


            conn.Close();
        }
        */
        public Boolean checkFriendID(string friendID)
        {
            
            if(friendID.Length != 8)
            {
                return false;
            }
    
            if(!char.IsLetter(friendID[0]))
            {
                return false;
            }

            for (int i = 1; i < 8; i++ )
            {
                if(!char.IsDigit(friendID[i]))
                {
                    return false;
                }
            }
            return true;
        }
        protected void addFriendButton_Click(object sender, EventArgs e)
        {
            string userName = userInfo.getUserName();
            string userID = userInfo.getUserID();
            string friendID = friendIdTextBox.Text.ToLower();
            string friendName = friendNameTextBox.Text;
            friendName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(friendName);

            if (friendName == "")
                warningLabel.Text = "No Name enter ";
            else if (friendID == "")
                warningLabel.Text = "No id enter";
            else if (checkFriendID(friendID) == false)
                warningLabel.Text = "Invalid friend ID";
            else
            {
                Boolean notFriend = true;

                SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT friendID, friendName FROM friendList where userId='" + userID + "'");
                String htmlCode = String.Empty;
                String clashName = "";
                foreach (DataRow _DataRow in _DataTable.Rows)
                {
                    if (_DataRow["friendID"].ToString() == friendID)
                    {
                        clashName = _DataRow["friendName"].ToString();
                        notFriend = false;
                        break;
                    }

                }

                if(friendID == userID)
                {
                    notFriend = false;
                }
                if (notFriend == true)
                {
                    SqlConnection conn = new SqlConnection();

                    conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                    conn.Open();


                    string insert = "INSERT into FriendList(userID,userName,friendID,friendName, isFriend, isAck, isAccepted) " + " VALUES ('" + userID + "', '" + userName + "', '" + friendID + "', '" + friendName + "','False','False','False')"
                        + "INSERT into FriendList(userID,userName,friendID,friendName, isFriend, isAck, isAccepted) " + " VALUES ('" + friendID + "', '" + friendName + "', '" + userID + "', '" + userName + "','False','False','False');";
                    SqlCommand cmd = new SqlCommand(insert, conn);


                    cmd.ExecuteNonQuery();


                    conn.Close();

                    //friendListBox.DataBind();
                    editNameListBox.DataBind();
                    warningLabel.Text = "";
                    friendIdTextBox.Text = "";
                    friendNameTextBox.Text = "";
                    editNameTextBox.Text = "";
                }
                else
                {
                    editNameListBox.DataBind();
                    warningLabel.Text = "Friend ID belongs to " + clashName;
                    friendIdTextBox.Text = "";
                    friendNameTextBox.Text = "";
                    editNameTextBox.Text = "";
                }
            }
        }


        /* from tesFriend */

        private string UpperFirst(string text)
        {
            return char.ToUpper(text[0]) +
                ((text.Length > 1) ? text.Substring(1).ToLower() : string.Empty);
        }
        
        public void addFriendToSql()
        {
            HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();

            string url = HttpContext.Current.Request.RawUrl;

            string userName = userInfo.getUserName();
            string userID = userInfo.getUserID();
            string friendID = "";
            string friendName = "";
            if (url.IndexOf('?') > -1)
            {
                int idIndex = url.IndexOf("id=");
                int nameIndex = url.IndexOf("name=");

                friendID = url.Substring(idIndex + 3, 8);
                friendID = friendID.ToLower();
                friendName = url.Substring(nameIndex + 5);
               


                friendName = friendName.Replace("%20", " ");
                friendName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(friendName.ToLower());
                //Response.Redirect(Request.Url.AbsoluteUri);


                try
                {
                    Boolean notFriend = true;

                    SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                    DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT friendID FROM friendList");
                    String htmlCode = String.Empty;

                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        if (_DataRow["friendID"].ToString() == friendID)
                        {
                            notFriend = false;
                            break;
                        }

                    }

                    if(friendID == userID)
                    {
                        notFriend = false;
                    }

                    if (notFriend == true)
                    {

                        SqlConnection conn = new SqlConnection();

                        conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                        conn.Open();

                        string insert = "INSERT into FriendList(userID,userName,friendID,friendName, isFriend, isAck, isAccepted) " + " VALUES ('" + userID + "', '" + userName + "', '" + friendID + "', '" + friendName + "','False','False','False')"
                            + "INSERT into FriendList(userID,userName,friendID,friendName, isFriend, isAck, isAccepted) " + " VALUES ('" + friendID + "', '" + friendName + "', '" + userID + "', '" + userName + "','False','False','False');";
                        SqlCommand cmd = new SqlCommand(insert, conn);


                        cmd.ExecuteNonQuery();


                        conn.Close();
                        Response.Redirect("Friend.aspx");
                    }
                }
                catch { }
            }

        }

        /*From New Chat */
        protected void chatFriendButton_Click(object sender, EventArgs e)
        {
            string userID = userInfo.getUserID();
            string userName = userInfo.getUserName();
            string friendID = editNameListBox.SelectedItem.Value;
            string friendName = editNameListBox.SelectedItem.Text;
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