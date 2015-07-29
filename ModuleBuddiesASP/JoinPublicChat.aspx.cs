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
    public partial class JoinPublicChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.AbsoluteUri;

            int nameIndex = url.IndexOf("?groupName=") + 11;
            int uidIndex = url.IndexOf("&uid=");
            int urlIndex = url.IndexOf("&url=");

            string name = url.Substring(nameIndex, uidIndex - nameIndex);
            string uid = url.Substring(uidIndex + 5, urlIndex - (uidIndex + 5));
            string myUrl = url.Substring(urlIndex + 5);

            name = name.Replace("%20", " ");

            Label1.Text = name;
            Label2.Text = uid;
            Label3.Text = myUrl;

            HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();

            string userID = userInfo.getUserID();
            string userName = userInfo.getUserName();

            Boolean isJoin = false;

            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT UniqueID FROM PublicChatList WHERE MemberID='" + userID + "'");
            String htmlCode = String.Empty;

            foreach (DataRow _DataRow in _DataTable.Rows)
            {
                if (_DataRow["UniqueID"].ToString() == uid)
                {
                    isJoin = true;
                    break;
                }

            }


            if (isJoin == false)
            {
                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                conn.Open();

                string insert = @"INSERT INTO PublicChatList(UniqueID, GroupName, MemberName, MemberID)
                        VALUES('" + uid + "','" + name + "','" + userName + "','" + userID + "');";

                SqlCommand insertCmd = new SqlCommand(insert, conn);

                insertCmd.ExecuteNonQuery();

                conn.Close();

                String newUrl = myUrl.Substring(0, myUrl.IndexOf('/')) + "/Chats.aspx";
                Response.Redirect("Chats.aspx");
            }
            else
            {
                String newUrl = myUrl.Substring(0, myUrl.IndexOf('/')) + "/Chats.aspx";
                Response.Redirect("Chats.aspx");
            }
        }
            
        
    }
}