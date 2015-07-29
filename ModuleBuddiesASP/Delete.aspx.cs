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
    public partial class Delete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.AbsoluteUri;

            int typeIndex = url.IndexOf("?type=") + 6;         
            int uidIndex = url.IndexOf("&uid=");
            int urlIndex = url.IndexOf("&url=");

            string type = url.Substring(typeIndex, uidIndex - typeIndex);
            string uid = url.Substring(uidIndex + 5, urlIndex-(uidIndex+5));
            string myUrl = url.Substring(urlIndex + 5);


            Label1.Text = type;
            Label2.Text = uid;
            Label3.Text = myUrl;

            HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();

            string userID = userInfo.getUserID();

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            

            if(type == "public")
            {
                conn.Open();

                string delete = "DELETE FROM PublicChatList  WHERE UniqueId='" + uid + "' AND MemberID='" + userID + "';";

                SqlCommand deleteCmd = new SqlCommand(delete, conn);

                deleteCmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect(myUrl);
            }
            if(type == "group")
            {
                conn.Open();

                string delete = "DELETE FROM PrivateChatList  WHERE UniqueId='" + uid + "' AND MemberID='" + userID + "';";

                SqlCommand deleteCmd = new SqlCommand(delete, conn);

                deleteCmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect(myUrl);
            }
            if(type == "individual")
            {
                conn.Open();

                string delete = "DELETE FROM IndividualChatList  WHERE friendID='" + uid + "' AND UserID='" + userID + "';";

                SqlCommand deleteCmd = new SqlCommand(delete, conn);

                deleteCmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect(myUrl);
            }

            if(type == "doc")
            {
                conn.Open();

                string delete = "DELETE FROM Documents WHERE docID='" + uid + "' AND memberID='" + userID + "';";

                SqlCommand deleteCmd = new SqlCommand(delete, conn);

                deleteCmd.ExecuteNonQuery();

                conn.Close();
                Response.Redirect(myUrl);
            }
          
        }
    }
}