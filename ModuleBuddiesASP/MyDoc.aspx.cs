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
    public partial class MyDoc : System.Web.UI.Page
    {
        HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
           string userID = userInfo.getUserID();


           HelperClasses.Token myToken = new HelperClasses.Token();

           if (myToken.getToken() == "")
           {
               Response.Redirect("Home.aspx");
           }
            
                try
                {
                    string url = Request.Url.AbsoluteUri;

                    int docNameIndex = url.IndexOf("?docName=") + 9;
                    int docIdIndex = url.IndexOf("&docId=");
                    int userIdIndex = url.IndexOf("&userId=");

                    string strDocName = url.Substring(docNameIndex, docIdIndex - docNameIndex);
                    string strDocID = url.Substring(docIdIndex + 7, userIdIndex - (docIdIndex + 7));
                  
                    //docTitleLabel.Text = strDocName.Replace("%20", " ");
                    docTitleLabel.Text = Server.UrlDecode(Request.QueryString["docName"]);
                    //renameTextBox.Value = strDocName;
                    
                    SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                    DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT memberID FROM documents WHERE docId = '" + strDocID + "'");

                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        if (_DataRow["memberID"].ToString() == userID)
                        {
                            SqlConnection conn = new SqlConnection();

                            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                            conn.Open();

                            string selectData = "SELECT data FROM myDoc WHERE docID='" + strDocID + "';";
                            SqlCommand cmd = new SqlCommand(selectData, conn);

                            SqlDataReader dataReader = null;

                            dataReader = cmd.ExecuteReader();

                            string data = null;

                            while (dataReader.Read())
                            {
                                data = dataReader["data"].ToString();
                            }

                            myText.Value = data;

                            break;
                        }
                    }
                   
                }
                catch { }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            string userName = userInfo.getUserName();
            string userID = userInfo.getUserID();

            string url = Request.Url.AbsoluteUri;
            int docNameIndex = url.IndexOf("?docName=") + 9;
            int docIdIndex = url.IndexOf("&docId=");
            int userIdIndex = url.IndexOf("&userId=");

            string docName = url.Substring(docNameIndex, docIdIndex - docNameIndex);
            string docId = url.Substring(docIdIndex + 7, userIdIndex - (docIdIndex + 7));

            //docName = docName.Replace("+", " ");
            docName = Server.UrlDecode(Request.QueryString["docName"]);

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            conn.Open();

            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT memberID, memberName FROM Documents where docId='" + docId + "'");


           
          
            foreach (int i in friendListBox.GetSelectedIndices())
            {
                Boolean notFriend = true;

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
                    string insertFriend = "INSERT into Documents(docName, memberID, memberName, docId) VALUES(N'"
                        + docName + "','" + friendListBox.Items[i].Value + "','" + friendListBox.Items[i].Text + "','" + docId + "');";
                    SqlCommand cmdFriend = new SqlCommand(insertFriend, conn);
                    cmdFriend.ExecuteNonQuery();
                }

                

            }

            usersListBox.DataBind();

            string newUrl = url.Substring(0, docNameIndex) +
               docName + "&docId=" + docId +"&userId=" + userID;

            Response.Redirect(newUrl);
            
        }
        protected void deleteButton_Click(object sender, EventArgs e)
        {
            string url = Request.Url.AbsoluteUri;
            string userID = userInfo.getUserID();

            int docNameIndex = url.IndexOf("?docName=") + 9;
            int docIdIndex = url.IndexOf("&docId=");
            int userIdIndex = url.IndexOf("&userId=");

            string strDocName = url.Substring(docNameIndex, docIdIndex - docNameIndex);
            string strDocID = url.Substring(docIdIndex + 7, userIdIndex - (docIdIndex + 7));
            strDocName = strDocName.Replace("+", " ");

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            conn.Open();

            foreach (int i in usersListBox.GetSelectedIndices())
            {
                string delete = "DELETE FROM Documents WHERE memberID='" + usersListBox.Items[i].Value +
                    "' AND memberName='" + usersListBox.Items[i].Text + "' AND docId='" + strDocID + "';";

                SqlCommand cmdFriend = new SqlCommand(delete, conn);
                cmdFriend.ExecuteNonQuery();
            }

            usersListBox.DataBind();

            string newUrl = url.Substring(0, docNameIndex) +
               strDocName.Replace("+", "%20") + "&docId=" + strDocID + "&userId=" + userID;

            Response.Redirect(newUrl);
        }

        protected void rename_Click(object sender, EventArgs e)
        {
            if (renameTextBox.Value != "")
            {
                string userID = userInfo.getUserID();
                string url = Request.Url.AbsoluteUri;
                int docNameIndex = url.IndexOf("?docName=") + 9;
                int docIdIndex = url.IndexOf("&docId=");
                int userIdIndex = url.IndexOf("&userId=");

                string docName = url.Substring(docNameIndex, docIdIndex - docNameIndex);
                string docId = url.Substring(docIdIndex + 7, userIdIndex - (docIdIndex + 7));

                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                conn.Open();

                string update = "UPDATE myDoc SET docName =N'" + renameTextBox.Value + "' WHERE docId='"
                    + docId + "';";

                SqlCommand updateCmd = new SqlCommand(update, conn);

                updateCmd.ExecuteNonQuery();

                string update1 = "UPDATE documents SET docName =N'" + renameTextBox.Value + "' WHERE docId='"
                    + docId + "';";

                SqlCommand updateCmd1 = new SqlCommand(update1, conn);

                updateCmd1.ExecuteNonQuery();

                conn.Close();

                Response.Redirect(Request.Url.AbsoluteUri.Replace(Request.Url.Query, String.Empty) +
                    "?docName=" + renameTextBox.Value + "&docId=" + docId + "&userId=" + userID);
            }
        }
            
       
    }
}