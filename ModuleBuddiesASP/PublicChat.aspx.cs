using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ModuleBuddiesASP
{
    public partial class PublicChat : System.Web.UI.Page
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
                string url = Request.Url.AbsoluteUri;
                int index = url.IndexOf("?");
                int uidIndex = url.IndexOf("&uid=");
                string title = url.Substring(index + 5, uidIndex - (index + 5));
                title = title.Replace("%20", " ");
                publicChatLabel.Text = title;
            }
        }
    }
}