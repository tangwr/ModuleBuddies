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
    public partial class ManageChat : System.Web.UI.Page
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

                editGroupNameListBox.DataBind();
                editGroupNameTextBox.Text = "";
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

                editPublicNameListBox.DataBind();
                editPublicNameTextBox.Text = "";
                
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

                editGroupNameListBox.DataBind();
                editGroupNameTextBox.Text = "";
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

                editPublicNameListBox.DataBind();
                editPublicNameTextBox.Text = "";
            }
            catch { }
        }
    }
}