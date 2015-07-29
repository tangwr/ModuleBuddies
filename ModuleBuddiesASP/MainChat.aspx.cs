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
    public partial class MainChat : System.Web.UI.Page
    {
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

                    int uidIndex = url.IndexOf("?uid=") + 5;
                    int fidIndex = url.IndexOf("&fid=");

                    string uid = url.Substring(uidIndex, fidIndex - uidIndex);
                    string fid = url.Substring(fidIndex + 5);



                    int index = url.IndexOf("&fid=");

                    HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();

                    string userID = userInfo.getUserID();

                    string friendID = url.Substring(index + 5);

                    if (uid == userID)
                    {
                        SqlConnection conn = new SqlConnection();

                        conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                        conn.Open();

                        string insert = "SELECT(friendName) FROM FriendList WHERE userID='" + userID + "' and friendID='" + friendID + "'";
                        SqlCommand cmd = new SqlCommand(insert, conn);

                        chatTitleLabel.Text = (string)cmd.ExecuteScalar();

                        conn.Close();
                    }
                    else
                    {
                        Response.Redirect("Chats.aspx");
                    }
                }
                catch
                {
                    Response.Redirect("Chats.aspx");
                }
              
            }
           
           
        }
    }
}