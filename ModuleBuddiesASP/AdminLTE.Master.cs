using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;

namespace ModuleBuddiesASP
{
    public partial class AdminLTE : System.Web.UI.MasterPage
    {
        #region public variables
        //public static String token = "";   
        public static String ivleUrl = "https://ivle.nus.edu.sg/api/Lapi.svc/";
        public static String apiKey = "APIKey=JinwNKRnSNSXfoTIhY1X0";
        //public static String strUsername = "";
        #endregion

        #region private variables
        //private static Boolean isAuthenticated = false;
        //private static Boolean isLoginClick = false;
        HttpCookie isAuthCookie = new HttpCookie("isAuthCookie");
        HttpCookie isLoginCookie = new HttpCookie("isLoginCookie");
        HttpCookie tokenCookie = new HttpCookie("tokenCookie");
        HttpCookie userCookie = new HttpCookie("userCookie");
        HttpCookie userIdCookie = new HttpCookie("userIdCookie");

       
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
           /*
                if (isLoginClick ==true)
                {
                    if (isAuthenticated == false)
                    {
                        ivleToken();

                    }
                }
           */ 
            string url = HttpContext.Current.Request.RawUrl;

            if(url.Contains("?token="))
            {
                ivleToken();
            }
            loginLabel.Text = "Login";
            userLabel.Text = "Login";

            HelperClasses.Token tok = new HelperClasses.Token();
            string token = tok.getToken();
            if (token != "")
            {
                loginLabel.Text = Username();
                userLabel.Text = Username();
                visibleLink();
            }
            else
            {
                loginLabel.Text = "Login";
                userLabel.Text = "Login";
                nonVisibleLink();
            }
           
        }

        protected void loginLinkButton_Click(object sender, EventArgs e)
        {
            string apikey = "JinwNKRnSNSXfoTIhY1X0";
            string url = "https:///ivle.nus.edu.sg/api/login/?apikey=" + apikey + "&url=";
           
            //if (isLoginClick == false)
            if(loginLabel.Text.Equals("Login"))
            {
                //isLoginClick = true;           
                Response.Redirect(url + Request.UrlReferrer);
            }
            else
            {          
                //deleteCookie();    
                Response.Redirect("Home.aspx");
            }

        }

        protected void userLinkButton_Click(object sender, EventArgs e)
        {
            string apikey = "JinwNKRnSNSXfoTIhY1X0";
            string url = "https:///ivle.nus.edu.sg/api/login/?apikey=" + apikey + "&url=";

            
            //if (isLoginClick == false)
            if(userLabel.Text.Equals("Login"))
            {
                //isLoginClick = true;
                Response.Redirect(url + Request.UrlReferrer);
            }
            else
            {
                //deleteCookie();
                Response.Redirect("Home.aspx");
            }
        }

        protected void friendsLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Friend.aspx");
        }

        protected void chatLinkButton_Click(object sender, EventArgs e)
        {
            HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();
            string userID = userInfo.getUserID();
            Response.Redirect("GroupChat.aspx");
        }

        protected void documentsLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Documents.aspx");
        }

        protected void logoutLinkButton_Click(object sender, EventArgs e)
        {
            deleteCookie();
            Response.Redirect("Home.aspx");
        }
        private void sqlConnection()
        {
            /*
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = @"Server=tcp:yq6ulqknjf.database.windows.net,1433;Database=ModulesDB;User ID=rstyle@yq6ulqknjf;Password=Zxcv2345;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

            conn.Open();

            string insert = "INSERT into FriendList(userID,userName,friendID,friendName, isFriend, isAck, isAccepted) " + " VALUES ('" + userID + "', '" + userName + "', '" + friendID + "', '" + friendName + "','False','False','False')"
                + "INSERT into FriendList(userID,userName,friendID,friendName, isFriend, isAck, isAccepted) " + " VALUES ('" + friendID + "', '" + friendName + "', '" + userID + "', '" + userName + "','False','False','False');";
            SqlCommand cmd = new SqlCommand(insert, conn);


            cmd.ExecuteNonQuery();


            conn.Close();
             * */
        }
        public void deleteCookie()
        {
            //nonVisibleLink();

            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(aCookie);
            }

            //isLoginClick = false;
            //isAuthenticated = false;
            //Response.Redirect(Request.RawUrl);
            Response.Redirect("Home.aspx");
        }
        private void visibleLink()
        {
            //friendsLinkButton.Visible = true;
            //chatLinkButton.Visible = true;
            //documentsLinkButton.Visible = true;
            logoutLinkButton.Visible = true;
            friendsLink.Visible = true;
            chatLink.Visible = true;
            documentsLink.Visible = true;
            navFriendLink.Visible = true;
            navChatLink.Visible = true;
            navDocLink.Visible = true;
            navLogoutLinkButton.Visible = true;
        }

        public void getVisibleLink()
        {
            visibleLink();
        }
        private void nonVisibleLink()
        {
            //friendsLinkButton.Visible = false;
            //chatLinkButton.Visible = false;
            //documentsLinkButton.Visible = false;
            logoutLinkButton.Visible = false;
            friendsLink.Visible = false;
            chatLink.Visible =false;
            documentsLink.Visible = false;
            navFriendLink.Visible = false;
            navChatLink.Visible = false;
            navDocLink.Visible = false;
            navLogoutLinkButton.Visible = false;

        }
        public void getNonVisibleLink()
        {
            nonVisibleLink();
        }

        public void ivleToken()
        {
            string url = Request.RawUrl;
            
            HelperClasses.Cookies myCookies = new HelperClasses.Cookies();

            tokenCookie = myCookies.getTokenCookie();
            Response.Cookies.Add(tokenCookie);

            userCookie = myCookies.getUserInfoCookie();
            Response.Cookies.Add(userCookie);

            userIdCookie = myCookies.getUserIdCookie();
            Response.Cookies.Add(userIdCookie);

            Response.Redirect(Request.Url.AbsolutePath);
            /*
            if (isLoginClick == true)
            {
                //isLoginClick = false;
                //Response.Redirect(Request.RawUrl);
                isAuthenticated = true;
                Response.Redirect(Request.Url.AbsolutePath);
                //Response.Redirect("http:///modulebuddies.azurewebsites.net/");
            }
            */
        }

        private string Username()
        {
           
            string username = "Login";
            string id = "";
            string email = "";
         
            if (Request.Cookies["userCookie"] != null)
            {
                //username = Server.HtmlEncode(Request.Cookies["userCookie"].Value);

                NameValueCollection UserInfoCookieCollection = new NameValueCollection();

                UserInfoCookieCollection = Request.Cookies["userCookie"].Values;
                username = Server.HtmlEncode(UserInfoCookieCollection["username"]);
                id = Server.HtmlEncode(UserInfoCookieCollection["userID"]);
                email = Server.HtmlEncode(UserInfoCookieCollection["userEmail"]);

                return username;
            }
            else
            {
                return username;
            }

        }

    }
      
}