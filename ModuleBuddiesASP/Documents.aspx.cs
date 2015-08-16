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
    public partial class Documents : System.Web.UI.Page
    {
        HelperClasses.ModuleInfo modInfo = new HelperClasses.ModuleInfo();
        HelperClasses.CourseID course = new HelperClasses.CourseID();
        HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();
        /*
        List<String> ListModCode = new List<String>();
        List<String> ListModName = new List<String>();
        List<String> ListCourseSem = new List<String>();
        List<String> ListAcadYear = new List<String>();
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            HelperClasses.Token myToken = new HelperClasses.Token();

            if (myToken.getToken() == "")
            {
                Response.Redirect("Home.aspx");
            }
            /*
            ListModCode = modInfo.getModInfo("CourseCode");
            ListModName = modInfo.getModInfo("CourseName");
            ListCourseSem = modInfo.getSemNum(modInfo.getModInfo("CourseSemester"));
            ListAcadYear = modInfo.getModInfo("CourseAcadYear");
            */
            documentListDiv.Controls.Add(new LiteralControl() { Text = getDocumentListHtml() });
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {

                string userID = userInfo.getUserID();

                string selectedDoc = docListBox.SelectedItem.Value;
                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

                conn.Open();

                string insert = "DELETE FROM documents WHERE memberID='" + userID + "' AND docID='"
                    + selectedDoc + "';";
                SqlCommand cmd = new SqlCommand(insert, conn);

                cmd.ExecuteNonQuery();

                conn.Close();

                docListBox.DataBind();
            }
            catch { }
        }

        protected void createButton_Click(object sender, EventArgs e)
        {
            string userName = userInfo.getUserName();
            string userID = userInfo.getUserID();
            Guid UniqueID = Guid.NewGuid();

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            conn.Open();

            string insertCreater = "INSERT into Documents(docName, memberID, memberName, docId) VALUES(N'"
                    + docTextBox.Value + "','" + userID + "','" + userName + "','" + UniqueID + "');";
            SqlCommand cmdCreater = new SqlCommand(insertCreater, conn);
            cmdCreater.ExecuteNonQuery();

            foreach(int i in friendListBox.GetSelectedIndices())
            {
                string insertFriend = "INSERT into Documents(docName, memberID, memberName, docId) VALUES(N'"
                    + docTextBox.Value + "','" + friendListBox.Items[i].Value + "','" + friendListBox.Items[i].Text + "','" + UniqueID + "');";
                SqlCommand cmdFriend = new SqlCommand(insertFriend, conn);
                cmdFriend.ExecuteNonQuery();
            }

            string insertDoc = "INSERT into myDoc(docID, docName) VALUES('"
                    + UniqueID + "',N'" + docTextBox.Value + "');";
            SqlCommand cmdDoc = new SqlCommand(insertDoc, conn);
            cmdDoc.ExecuteNonQuery();

            docListBox.DataBind();

            Response.Redirect("Documents.aspx");
          
        }

        protected void openButton_Click(object sender, EventArgs e)
        {
            string userID = userInfo.getUserID();

            Response.Redirect("MyDoc.aspx?docName=" + docListBox.SelectedItem.Text + "&docId="
                + docListBox.SelectedItem.Value + "&userId=" + userID);
        }

        public String getDocumentListHtml()
        {
            string userID = userInfo.getUserID();

            SqlWrapper _SqlWrapper = new SqlWrapper(@"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            DataTable _DataTable = _SqlWrapper.executeQuery(@"SELECT docName, docId FROM documents WHERE memberID='" + userID + "'");
            String htmlCode = String.Empty;

            
            foreach (DataRow _DataRow in _DataTable.Rows)
            {

                htmlCode += @"<tr>
                                <td>" + _DataRow["docName"].ToString() + @"</td>   
                                <td><a ID=""openDocLink"" runat=""server""  style=""cursor: pointer;""
                                        onclick=""openDoc('" + _DataRow["docName"].ToString()  + "','" + _DataRow["docId"].ToString() + "','" + userID + @"')"" >Open</a></td>     
                                 <td><a ID=""deleteLink"" runat=""server""  style=""cursor: pointer;""
                                        onclick=""deleteDoc('doc','" + _DataRow["docId"].ToString() + "','" + userID + @"')"" >Delete</a></td></tr>";
            }

            return htmlCode;

        }
    }
}