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
    public partial class ChatPublic : System.Web.UI.Page
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

        public String getPublicChatListHtml()
        {
            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT uniqueID, groupName, authorName FROM publicChatAuthor");
            String htmlCode = String.Empty;

            foreach (DataRow _DataRow in _DataTable.Rows)
            {

                htmlCode += @"<tr><td>" + _DataRow["groupName"].ToString() + @"</td>" +
                            @"<td>" + _DataRow["authorName"].ToString() + @"</td>"
                         + @"<td><a ID=""pubChatGrp"" runat=""server""  style=""cursor: pointer;""
                        onclick=""joinPublicChat('" + _DataRow["uniqueID"].ToString() + "','" + _DataRow["groupName"].ToString() + @"')"">Join</a> </td></tr>";
            }

            return htmlCode;

        }
    }
}